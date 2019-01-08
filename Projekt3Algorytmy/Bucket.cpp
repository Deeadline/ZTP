#include "pch.h"
#include "Bucket.h"



Bucket::Bucket(int size, int distance)
{
	this->distance = distance;
	MyArray.resize(size);
	Fill();
}

Bucket::~Bucket()
{
}

void Bucket::FindPairs()
{
	int pairs = 0;
	auto start = std::chrono::system_clock::now();

	Hash();

	for (auto &a : Buckets) //sprawdzanie elementow
	{
		if (a.second.size() > 1) //jesli jest wiecej elementow niz jeden
		{
			for (auto lists = a.second.begin(); lists != a.second.end() - 1; ++lists) //bierzemy liste z pierwszego kubelka - pierwszy element
			{
				for (auto lists2 = lists + 1; lists2 != a.second.end(); ++lists2) // bierzemy drugi element listy
				{
					//std::cout << "Para punktow: " << MyArray[*lists] << ", " << MyArray[*lists2] << std::endl;
				}
			}
			pairs = pairs + ((a.second.size()*(a.second.size() - 1)) / 2); //wszystkie pary
		}

		auto lists1 = Buckets.upper_bound(a.first); //przeskakuje po indeksach list

		if (lists1 != Buckets.end()) //jezeli indeksy nie sa koncem
		{
			for (auto lists = a.second.begin(); lists != a.second.end(); ++lists) //bierzemy elementy z listy pierwszgo kubelka
			{
				for (auto lists2 = lists1->second.begin(); lists2 != lists1->second.end(); ++lists2) //bierzemy elementy z listy drugiego kubelka
				{
					if ((abs(MyArray[*lists] - MyArray[*lists2]) < distance) && (abs(MyArray[*lists] - MyArray[*lists2]) > 0)) //porownujemy odeglosc i sprawdzamy wystepowanie powtorzen
					{
						//std::cout << "Para punktow: " << MyArray[*lists] << ", " << MyArray[*lists2] << std::endl;
						pairs++;
					}
				}
			}
		}
	}

	auto end = std::chrono::system_clock::now();
	std::chrono::duration<double> elapsed_seconds = end - start;
	std::cout << "Time: " << elapsed_seconds.count() << "s\n";
	std::cout << "Ilosc par: " << pairs << "\n";
}

void Bucket::Fill()
{
	for (int i = 0; i < MyArray.size(); i++)
	{
		MyArray[i] = (std::rand() % MyArray.size()) - (std::rand() % MyArray.size());
	}
}

void Bucket::Print() const
{
	for (int i = 0; i < MyArray.size(); i++)
	{
		std::cout << "MyArray [ " << i << " ] = " << MyArray[i] << "\n";
	}
}

void Bucket::Hash()
{
	for (int i = 0; i < MyArray.size(); i++)
	{
		Buckets[floor(MyArray[i] / distance)].push_back(i);
	}
}


