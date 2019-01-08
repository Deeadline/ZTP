#include "pch.h"
#include <iostream>

int main()
{
	Bucket* bucket = new Bucket(100, 3);
	bucket->FindPairs();

	//QuickSort* quickSort = new QuickSort(100, 3);
	//quickSort->FindPairs();

	delete bucket;
	//delete quickSort;

	return 0;
}
