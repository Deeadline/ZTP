#include "pch.h"
#include <iostream>

int main()
{
	Bucket* bucket = new Bucket(10000, 500);
	bucket->FindPairs();

	QuickSort* quickSort = new QuickSort(10000, 500);
	quickSort->FindPairs();

	delete bucket;
	delete quickSort;

	return 0;
}
