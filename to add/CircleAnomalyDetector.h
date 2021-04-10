
#ifndef CIRCLEANOMALYDETECTOR_H_
#define CIRCLEANOMALYDETECTOR_H_

#include "anomaly_detection_util.h"
#include "AnomalyDetector.h"
#include "minCircle.h"
#include <vector>
#include <algorithm>
#include <string.h>
#include <math.h>


struct correlatedFeatures {
	string feature1, feature2;                     // names of the correlated features.
	float corrlation;                             // the correlation between the features.
	float threshold;                              // the maximum valid deviation from the lin_reg or the center point of the min_circle
	Circle min_circle = *(new Circle(Point(0, 0), 0)); // if the correlation is < 0.9 but > 0.5: the minimum circle contains all the points

	/**
	 * constructor.
	 **/
	correlatedFeatures(string feature1, string feature2,
		float correlation, Point** points, int size) {

		this->feature1 = feature1;
		this->feature2 = feature2;
		this->corrlation = correlation;
		this->min_circle = findMinCircle(points, size);
		this->threshold = min_circle.radius;

	}
};



class CircleAnomalyDetector :public TimeSeriesAnomalyDetector {
protected:
	vector<correlatedFeatures> cf;      // the correlated features that was found in learn normal level
	float min_correlation_circle = 0;  // the minimum correlation required for circle correlation

public:
	/**
	 * Constructor.
	 **/
	CircleAnomalyDetector();
	/**
	 * Destructor.
	 **/
	virtual ~CircleAnomalyDetector();

	/**
	 * this method gets a time series and initilaize this.cf
	 * to be the vector of the correlated features in the time series given.
	 **/
	virtual void learnNormal(const TimeSeries& ts);
	/**
	 * this method gets a time series and returns a vector of anomaly reports,
	 * based on the level of learnNormal.
	 **/
	virtual vector<AnomalyReport> detect(const TimeSeries& ts);

	/**
	 * getter.
	 * returns the corrlelated features.
	 **/
	vector<correlatedFeatures> getNormalModel() {
		return this->cf;
	}

	/**
	 * this method returns the distance of the point p from
	 * the center of the minimum circle, or the deviation of p from the linear_reg
	 * according to the given correlation
	 */
	float findDistFromNormal(correlatedFeatures corr, Point& p);

	/**
	 * this method returns a pointer to a correlated features that fits the given arguments.
	 * (minCircle correlation if the correlation is in range 0.5-0.9,
	 *  and linear correlation if the correlation is 0.9 and above)
	 */
	correlatedFeatures* make_correlated_features(string feature1, string feature2,
		float correlation, Point** points, int size);

	/**
	 * this method returns 0 if the correlation is under min_correlation_circle and 1 otherwise.
	 */
	int is_above_req_correlation(float correlation);

	/**
	 * the function creates an array of points from the
	 * given array of x and y in size of size and returns a pointer to the array.
	 **/
	static Point** createArrayPoints(float* x, float* y, int size);

	/**
	 * this method returns 1 whether the current_dev
	 * is not grater than 1.1*correct_dev_threshold and 0 otherwise.
	 **/
	int isNotDevValid(float current_dev, float correct_dev_threshold);

};

#endif /* CIRCLEANOMALYDETECTOR_H_ */
