
/*
 * timeseries.h
 *
 * Author: 322439266 Noa Levy
 */

#ifndef TIMESERIES_H_
#define TIMESERIES_H_
#include "map"
#include "vector"
#include <string>

using namespace std;

class TimeSeries {

	map<string, vector<float>> table;
	vector<string> titles;

	/**
	 * this method gets an input file stream and initilaize
	 * the member titles to the first row of the file.
	 **/
	void getTitles(ifstream& fin);

	/**
	 * this method gets a string vector of the first row
	 * and initializes this.table keys to be the strings in the given array.
	 **/
	void initTitlesToTable(vector<string> titles);

	/**
	 * this method gets an input file stream and a string vector of the first row and
	 * initializes this.table values to be the values from the given stream.
	 **/
	void initValuesToTable(ifstream& fin, vector<string> titles);

	/**
	 * this method gets a stringstream and a string vector of the first row and
	 * init the row from fin (the stream) to this.table.
	 **/
	void initRowToTable(stringstream& fin, vector<string> titles);

public:

	/**
	 * constructor.
	 * the function gets a name of a file and initilaize the members of timeseries:
	 * the first row of the files will be the titles and the keys in the table map,
	 * each of the rows in the rest of the file will be the values of the diferret titles in a time step.
	 **/
	TimeSeries(const char* CSVfileName);

	/**
	 * getter.
	 * returns this.titles as a const vector.
	 **/
	vector<string> getTitles() const;

	/**
	 * getter.
	 * returns this.table as a const map.
	 **/
	map<string, vector<float>> getTable() const;

	/**
	 * this method returns the values (vector<float>) of the given feature.
	 **/
	vector<float> getValuesAt(string feature) const;

	/**
	 * this method returns the number of the rows in timeseries table.
	 * (number of time steps - not includding the titles row)
	 **/
	int getNumRows() const;

};



#endif /* TIMESERIES_H_ */
