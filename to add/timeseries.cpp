/*
 * timeseries.cpp
 *
 * Author: 322439266 Noa Levy
 */

#include "timeseries.h"
#include <iostream>
#include <sstream>
#include <fstream>
#include <string> 
#include <map>

using namespace std;

TimeSeries::TimeSeries(const char* CSVfileName) {
	ifstream fin;
	fin.open(CSVfileName);
	if (!fin.is_open()) {
		cerr << "error: open file for input failed!" << endl;
		abort();
	}

	// titles holds a vector of the titles.
	this->getTitles(fin);
	// this.table will now initialize it's keys.
	this->initTitlesToTable(this->titles);
	// this.table will now initialize it's values.
	this->initValuesToTable(fin, titles);
	fin.close();
}

void TimeSeries::getTitles(ifstream& fin) {
	// get the first row of the file.
	string first_row;
	getline(fin, first_row, '\n');
	// seperate the features by the char ','
	// and each feature push to the title vector
	stringstream sfr(first_row);
	string feature;
	while (getline(sfr, feature, ',')) {
		this->titles.push_back(feature);
	}
}

void TimeSeries::initTitlesToTable(vector<string> titles) {
	int size = titles.size();
	vector<float> fl;
	// init the table- the keys are the features in the titles
	// the values are init to be empty.
	for (int i = 0; i < size; i++) {
		this->table.insert(pair<string, vector<float>>(titles[i], fl));
	}
}

void TimeSeries::initRowToTable(stringstream& str_row, vector<string> tepmTitles) {
	int size = tepmTitles.size();
	string fl;
	int i = 0;
	while (getline(str_row, fl, ','))
	{
		string key = tepmTitles[i % size];
		i++;
		// converting string fl to float 
		float new_val = stof(fl);
		this->table.at(key).push_back(new_val);
	}
}

void TimeSeries::initValuesToTable(ifstream& fin, vector<string> tempTitiles) {
	string row;
	// seperate the rows and each of them init to the table.
	while (getline(fin, row))
	{
		stringstream s_row(row);
		initRowToTable(s_row, tempTitiles);
	}
}

vector<string> TimeSeries::getTitles() const {
	const vector<string> const_titles = this->titles;
	return(const_titles);
}

map<string, vector<float>> TimeSeries::getTable() const {
	const map<string, vector<float>> const_table = this->table;
	return(const_table);
}

vector<float> TimeSeries::getValuesAt(string feature) const {
	const vector<float> const_values = this->table.at(feature);
	return(const_values);
}

int TimeSeries::getNumRows() const {
	int rows = (this->getValuesAt(getTitles()[0])).size();
	return rows;
}