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
	Bucket(int size, int distance);
	~Bucket();
	void FindPairs();
private:
	std::map<int, std::vector<int>> Buckets;
	int distance;
	std::vector<int> MyArray;

	void Fill();
	void Print() const;
	void Hash();
};

