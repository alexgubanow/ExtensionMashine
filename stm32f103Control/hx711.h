#pragma once
#include "pin.h"
class HX711
{
public:
	struct pinsStruct
	{
		pin CLK;
		pin DATA;
	} _pins;

	int _offset;
	// 1: channel A, gain factor 128
	// 2: channel B, gain factor 32
	// 3: channel A, gain factor 64
	enum hx711Gain
	{
		_128 = 1,
		_64 = 2,
		_32 = 3
	}_gain;
	HX711();
	HX711(pinsStruct pins, hx711Gain gain);
	~HX711();
	bool is_ready();
	long read();
	long read_average(int times);
	double HX711::get_value(int times);
	float HX711::get_units(int times);
};

