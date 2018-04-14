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

	long  _offset;
	float SCALE;	// used to return weight in grams, kg, ounces, whatever
	// 1: channel A, gain factor 128
	// 2: channel B, gain factor 32
	// 3: channel A, gain factor 64
	enum hx711Gain
	{
		_128 = 1,
		_64 = 3,
		_32 = 2
	}_gain;
	HX711();
	HX711(pinsStruct pins, hx711Gain gain);
	~HX711();
	bool is_ready();
	long read();
	long read_average(int times);
	int get_value(int times);
	int get_units(int times);
	void set_offset(long offset);
	void set_scale(float scale);
	void tare(int times);
};

