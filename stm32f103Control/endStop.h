#pragma once
#include "stm32f1xx_hal.h"
#include "pin.h"
class endStop
{
public:
	pin _pin;
	endStop() {};
	endStop(GPIO_TypeDef* port, unsigned short EndStopPin)
	{
		_pin = pin(port, EndStopPin);
	}
	inline unsigned char get() { return _pin.get(); };
};

