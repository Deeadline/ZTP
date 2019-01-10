#pragma once
#include <iostream>
#include <vector>
#include <iterator>
#include <math.h>
#include <chrono>
#include <ctime>
class QuickSort
{
public:
	QuickSort(long int size, long int distance);
	~QuickSort();
	void FindPairs();

private:
	void Sort(std::vector<long int> myArray, long int a, long int b);
	int Split(std::vector<long int> myArray, long int a, long int b);
	std::vector<long int> Fill();
	long int R;
	size_t size;
	long int pairs;
};

