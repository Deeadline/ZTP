#include "pch.h"
#include "QuickSort.h"


QuickSort::QuickSort(int size, int distance)
{
	this->distance = distance;
	this->size = size;
}

QuickSort::~QuickSort()
{
}

void QuickSort::FindPairs()
{
	std::vector<int> myArray = Fill();
	clock_t start = clock();

	Sort(myArray, 0, size - 1);

	for (int i = 0; i < size - 1; i++)
	{
		for (int j = i + 1; j < size; j++)
		{
			if (std::abs(myArray[i] - myArray[j]) >= distance)
			{
				break;
			}
			else
			{
				std::cout << "Para punktow: " << myArray[i] << ", " << myArray[j] << "\n";
			}
		}
	}
	clock_t end = clock();

	double time = ((double)(end - start)) / CLOCKS_PER_SEC;
	std::cout << "Time: " << time;
}

void QuickSort::Sort(std::vector<int> myArray, int a, int b)
{
	int q = 0;
	if (a < b)
	{
		q = Split(myArray, a, b);
		Sort(myArray, a, q);
		Sort(myArray, q + 1, b);
	}
}

int QuickSort::Split(std::vector<int> myArray, int a, int b)
{
	int element = myArray[a];
	int i = a;
	int j = b;
	int w = 0;
	while (true)
	{
		while (myArray[j] > element)
		{
			j--;
		}
		while (myArray[j] < element)
		{
			i++;
		}
		if (i < j)
		{
			w = myArray[i];
			myArray[i] = myArray[j];
			myArray[j] = w;
			i++;
			j--;
		}
		else
		{
			return j;
		}
	}
}

std::vector<int> QuickSort::Fill()
{
	std::vector<int> temp;
	temp.resize(size);
	for (int i = 0; i < size; i++)
	{
		temp[i] = (std::rand() % size) - (std::rand() % size);
	}
	return temp;
}
