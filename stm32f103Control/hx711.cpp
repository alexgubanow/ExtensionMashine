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
	// wait for the chip to become ready
	while (!is_ready()) {
	}

	unsigned long value = 0;
	uint8_t data[3] = { 0 };
	uint8_t filler = 0x00;

	// pulse the clock pin 24 times to read the data
	//data[2] = shiftIn(DOUT, PD_SCK, MSBFIRST);
	//data[1] = shiftIn(DOUT, PD_SCK, MSBFIRST);
	//data[0] = shiftIn(DOUT, PD_SCK, MSBFIRST);

	// set the channel and the gain factor for the next reading using the clock pin
	for (unsigned int i = 0; i < _gain; i++) {
		_pins.CLK.set(GPIO_PinState::GPIO_PIN_SET);
		_pins.CLK.set(GPIO_PinState::GPIO_PIN_RESET);
	}

	// Replicate the most significant bit to pad out a 32-bit signed integer
	if (data[2] & 0x80) {
		filler = 0xFF;
	}
	else {
		filler = 0x00;
	}

	// Construct a 32-bit signed integer
	value = (static_cast<unsigned long>(filler) << 24
		| static_cast<unsigned long>(data[2]) << 16
		| static_cast<unsigned long>(data[1]) << 8
		| static_cast<unsigned long>(data[0]));

	return static_cast<long>(value);
}

long HX711::read_average(int times) {
	long sum = 0;
	for (int i = 0; i < times; i++) {
		sum += read();
	}
	return sum / times;
}

double HX711::get_value(int times) {
	return read_average(times) - OFFSET;
}

float HX711::get_units(int times) {
	return get_value(times) / SCALE;
}

void HX711::tare(int times) {
	double sum = read_average(times);
	set_offset(sum);
}

void HX711::set_scale(float scale) {
	SCALE = scale;
}

float HX711::get_scale() {
	return SCALE;
}

void HX711::set_offset(long offset) {
	OFFSET = offset;
}

long HX711::get_offset() {
	return OFFSET;
}

void HX711::power_down() {
	digitalWrite(PD_SCK, LOW);
	digitalWrite(PD_SCK, HIGH);
}

void HX711::power_up() {
	digitalWrite(PD_SCK, LOW);
}