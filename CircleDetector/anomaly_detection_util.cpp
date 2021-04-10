/*
 * animaly_detection_util.cpp
 *
 * Author: 322439266 Noa Levy
 */

#include <math.h>
#include <stdlib.h>
#include "anomaly_detection_util.h"

float avg(float* x, int size) {
	float sum = 0;
	for (int i = 0; i < size; i++) {
		sum += x[i];
	}
	float av = sum / size;
	return av;
}

float var(float* x, int size) {
	float sum_pow = 0;
	for (int i = 0; i < size; i++) {
		sum_pow += pow(x[i], 2.0);
	}
	float exp1 = sum_pow / size;

	float exp2 = pow(avg(x, size), 2.0);
	return (exp1 - exp2);
}

float cov(float* x, float* y, int size) {
	float* arr_mult = new float[size];
	for (int i = 0; i < size; i++) {
		arr_mult[i] = x[i] * y[i];
	}
	float av_xy = avg(arr_mult, size);
	float av_x = avg(x, size);
	float av_y = avg(y, size);
	return (av_xy - (av_x * av_y));
}

float pearson(float* x, float* y, int size) {
	float sigma_x = sqrt(var(x, size));
	float sigma_y = sqrt(var(y, size));
	float cov_xy = cov(x, y, size);

	float res = (cov_xy) / (sigma_x * sigma_y);
	return res;
}

Line linear_reg(Point** points, int size) {
	float* x = new float[size];
	float* y = new float[size];
	for (int i = 0; i < size; i++) {
		x[i] = points[i]->x;
		y[i] = points[i]->y;
	}
	float a = cov(x, y, size) / var(x, size);
	float b = (avg(y, size) - (a * avg(x, size)));

	return Line(a, b);
}

Line linear_reg(float* x, float* y, int size) {
	float a = cov(x, y, size) / var(x, size);
	float b = (avg(y, size) - (a * avg(x, size)));

	return Line(a, b);
}

float dev(Point p, Point** points, int size) {
	Line line = linear_reg(points, size);
	float expected_y = line.f(p.x);
	float dev = fabs(expected_y - p.y);
	return dev;
}

float dev(Point p, Line l) {
	float expected_y = l.f(p.x);
	float dev = fabs(expected_y - p.y);
	return dev;
}

float findThreshold(Point** points, int size, Line reg) {
	float cur_deviation;
	float max_deviation = 0;
	for (int i = 0; i < size; i++) {
		cur_deviation = dev(*points[i], reg);
		if (cur_deviation > max_deviation) {
			max_deviation = cur_deviation;
		}
	}
	return max_deviation;
}


