#include "CircleAnomalyDetector.h"

CircleAnomalyDetector::CircleAnomalyDetector() {
}

CircleAnomalyDetector::~CircleAnomalyDetector() {
}

void CircleAnomalyDetector::learnNormal(const TimeSeries& ts) {

	const vector<string> titles = ts.getTitles();
	string feature1;
	Point** points = nullptr;
	int size;
	// for every fearture in the time series, find the most correlated feature,
	// if the correlation between them greater than required,
	// add the correlated features to this->cf.
	for (int i = 0; i < titles.size() - 1; i++) {
		feature1 = titles[i];
		vector<float> vec1 = ts.getTable().at(feature1);

		float cur_correlation = 0;
		float max_correlation = 0;
		string feature2;
		string max_feature;

		// find the most correlated feature to the 
		// current feature (feature1) from the next feature and on.
		for (int j = i + 1; j < titles.size(); j++) {
			feature2 = titles[j];
			vector<float> vec2 = ts.getTable().at(feature2);
			float* x = &vec1[0];
			float* y = &vec2[0];
			int z = vec1.size();
			cur_correlation = pearson(x, y, z);
			if (fabs(cur_correlation) > fabs(max_correlation)) {
				max_correlation = cur_correlation;
				max_feature = feature2;
				points = createArrayPoints(x, y, z);
				size = z;
			}
		}
		if (is_above_req_correlation(max_correlation)) {
			correlatedFeatures* corr = nullptr;
			corr = make_correlated_features(feature1, max_feature,
				max_correlation, points, size);
			this->cf.push_back(*corr);
		}
	}
}

vector<AnomalyReport> CircleAnomalyDetector::detect(const TimeSeries& ts) {
	int num_rows = ts.getNumRows();
	vector<AnomalyReport> reports;
	for (correlatedFeatures corr : this->cf) {
		for (int i = 0; i < num_rows; i++) {
			// gets the i'th value of the vector of the given feature.
			float x = ts.getValuesAt(corr.feature1)[i];
			float y = ts.getValuesAt(corr.feature2)[i];
			Point p(x, y);
			float dist_from_norm = findDistFromNormal(corr, p);
			// if the deviation of the point from the corrlated
			// features in the current timestep is grater than
			// 1.1 * threshold computed in learnNormal stage - make a report.
			if (isNotDevValid(dist_from_norm, corr.threshold)) {
				string descrip = corr.feature1 + "-" + corr.feature2;
				int time = i + 1;
				AnomalyReport report(descrip, time);
				reports.push_back(report);
			}
		}
	}
	return reports;
}

int CircleAnomalyDetector::isNotDevValid(float current_dist, float correct_dev_threshold) {
	if (current_dist <= (1.1 * correct_dev_threshold)) {
		return 0;
	}
	return 1;
}

int CircleAnomalyDetector::is_above_req_correlation(float correlation) {
	if (fabs(correlation >= this->min_correlation_circle)) {
		return 1;
	}
	return 0;
}

correlatedFeatures* CircleAnomalyDetector::make_correlated_features(string feature1, string feature2,
	float correlation, Point** points, int size) {

	correlatedFeatures* corr = nullptr;

	// if the given correlation is above the requiered
	// correlation for circle correlation make the correlation a circle.
	if (fabs(correlation) >= this->min_correlation_circle) {
		corr = new correlatedFeatures(feature1, feature2, correlation, points, size);
	}
	return corr;
}

float CircleAnomalyDetector::findDistFromNormal(correlatedFeatures corr, Point& p) {

	// if the correlation is circle, the distance from normal defined
	// as the distance between the point and the center of the circle
	Circle circle = corr.min_circle;
	Point center = circle.center;
	float dist_from_center = dist(p, center);
	return dist_from_center;

}

Point** CircleAnomalyDetector::createArrayPoints(float* x, float* y, int size) {
	Point** points = new Point * [size];
	for (int i = 0; i < size; i++) {
		points[i] = new Point(x[i], y[i]);
	}
	return points;
}
