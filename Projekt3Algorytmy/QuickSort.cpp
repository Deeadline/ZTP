#include "pch.h"
#include "QuickSort.h"


QuickSort::QuickSort(long int size, long int distance)
{
	this->R = distance;
	this->size = size;
	this->pairs = 0;
}

QuickSort::~QuickSort()
{
}

void QuickSort::FindPairs()
{
	std::vector<long int> myArray = Fill();
	auto start = std::chrono::system_clock::now();

	Sort(myArray, 0, size - 1);

	for (int i = 0; i < size - 1; i++)
	{
		for (int j = i + 1; j < size; j++)
		{
			if (std::abs(myArray[i] - myArray[j]) >= R)
			{
				break;
			}
			else
			{
				//std::cout << "Para punktow: " << myArray[i] << ", " << myArray[j] << "\n";
				pairs++;
			}
		}
	}
	auto end = std::chrono::system_clock::now();
	std::chrono::duration<double> elapsed_seconds = (end - start);
	std::cout << "Time: " << elapsed_seconds.count() << "s\n";
	std::cout << "Ilosc par: " << pairs << "\n";
}

void QuickSort::Sort(std::vector<long int> myArray, long int a, long int b)
{
	int q = 0;
	if (a < b)
	{
		q = Split(myArray, a, b);
		Sort(myArray, a, q);
		Sort(myArray, q + 1, b);
	}
}

int QuickSort::Split(std::vector<long int> myArray, long int a, long int b)
{
	long int x = myArray[a];
	long int i = a;
	long int j = b;
	long int w = 0;
	while (true)
	{
		while (myArray[j] > x)
		{
			j--;
		}
		while (myArray[i] < x)
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

std::vector<long int> QuickSort::Fill()
{
	std::vector<long int> temp;
	temp.resize(size);
	for (long int i = 0; i < size; i++)
	{
		temp[i] = (std::rand() % 20000) - (std::rand() % 20000);
	}
	return temp;
}
