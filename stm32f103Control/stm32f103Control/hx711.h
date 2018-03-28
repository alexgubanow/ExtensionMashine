#pragma once
#include <stm32f1xx_hal.h>
#include <stm32_hal_legacy.h>
class hx711
{
public:

	typedef struct _hx711
	{
		GPIO_TypeDef* gpioSck;
		GPIO_TypeDef* gpioData;
		uint16_t pinSck;
		uint16_t pinData;
		int offset;
		int gain;
		// 1: channel A, gain factor 128
		// 2: channel B, gain factor 32
		// 3: channel A, gain factor 64
	} HX711p;


	void HX711_Init(HX711p data);
	HX711p HX711_Tare(HX711p data, uint8_t times);
	int HX711_Value(HX711p data);
	int HX711_Average_Value(HX711p data, uint8_t times);

	hx711();
	~hx711();
};

