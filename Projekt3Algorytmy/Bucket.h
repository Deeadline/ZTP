#pragma once
#include <iostream>
#include <map>
#include <vector>
#include <iterator>
#include <math.h>
#include <chrono>
#include <ctime>

class Bucket
{
public:
	Bucket(long int size, long int distance);
	~Bucket();
	void FindPairs();
private:
	std::map<long int, std::vector<long int>> Buckets;
	long int R;
	std::vector<long int> MyArray;

	void Fill();
	void Hash();
};

