#pragma once
#include <iostream>
#include <vector>
#include <iterator>
#include <math.h>
#include <ctime>
class QuickSort
{
public:
	QuickSort(int size, int distance);
	~QuickSort();
	void FindPairs();

private:
	void Sort(std::vector<int> myArray, int a, int b);
	int Split(std::vector<int> myArray, int a, int b);
	std::vector<int> Fill();
	int distance;
	size_t size;
};

