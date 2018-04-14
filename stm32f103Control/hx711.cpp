#include "hx711.h"

HX711::HX711()
{
}

HX711::~HX711()
{
}


HX711::HX711(HX711::pinsStruct pins, HX711::hx711Gain gain) {
	_pins = pins;
	_gain = gain;
}

bool HX711::is_ready() {
	return _pins.DATA.get() == GPIO_PinState::GPIO_PIN_RESET;
}
long HX711::read() {
	int buffer;
	buffer = 0;
	while (!is_ready())
		;
	for (uint8_t i = 0; i < 24; i++)
	{
		_pins.CLK.set(GPIO_PIN_SET);
		buffer = buffer << 1;
		if (_pins.DATA.get())
		{
			buffer++;
		}
		_pins.CLK.set(GPIO_PIN_RESET);
	}
	for (int i = 0; i < _gain; i++)
	{
		_pins.CLK.set(GPIO_PIN_SET);
		_pins.CLK.set(GPIO_PIN_RESET);
	}
	buffer = buffer ^ 0x800000;
	return buffer;
}

long HX711::read_average(int times) {
	long sum = 0;
	for (int i = 0; i < times; i++) {
		sum += read();
	}
	return sum / times;
}

int HX711::get_value(int times) {
	return read_average(times) - _offset;
}

int HX711::get_units(int times) {
	return get_value(times) / SCALE;
}

void HX711::tare(int times) {
	double sum = read_average(times);
	set_offset(sum);
}

void HX711::set_scale(float scale) {
	SCALE = scale;
}

void HX711::set_offset(long offset) {
	_offset = offset;
}