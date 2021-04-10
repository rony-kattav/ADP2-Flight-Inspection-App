
/*
 * minCircle.h
 *
 * Author: Eli
 */
#ifndef MINCIRCLE_H_
#define MINCIRCLE_H_

#include <iostream>
#include <vector>
#include <stdlib.h>     /* srand, rand */
#include <time.h>       /* time */
#include <math.h>
#include "anomaly_detection_util.h"

using namespace std;


// ------------ DO NOT CHANGE -----------
//class Point{
//public:
//	float x,y;
//	Point(float x,float y):x(x),y(y){}
//};

class Circle {
public:
	Point center;
	float radius;
	Circle(Point c, float r) :center(c), radius(r) {}
};
// --------------------------------------


/**
 * this function returns the distance between point a and point b.
 **/
float dist(Point a, Point b);

/**
 * this function returns the circle determined by the two points a and b.
 **/
Circle from2points(Point a, Point b);

/**
 * this function returns the circle determined by the 3 points a, b and c.
 **/
Circle from3Points(Point a, Point b, Point c);

/**
 * this function returns the trivial circle determined by the points.
 **/
Circle trivial(vector<Point>& P);

/**
 * this function returns the minimum circle encluding all
 * the points in P with the points R on the boundary of the circle.
 * n represents the number of points in P that are not yet processed.
 **/
Circle welzl(Point** P, vector<Point> R, size_t n);

/**
 * this function returns the minimum circle encluding all the given n points.
 **/
Circle findMinCircle(Point** points, size_t size);

#endif /* MINCIRCLE_H_ */
