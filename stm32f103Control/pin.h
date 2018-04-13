#pragma once
#include "stm32f1xx_hal.h"
class pin
{
	unsigned short _pin;
	GPIO_TypeDef* _port;
	GPIO_PinState state;
public:
	pin() {};
	pin(GPIO_TypeDef* port, unsigned short pin);
	inline GPIO_PinState get() { state = HAL_GPIO_ReadPin(_port, _pin); return state; };
	inline void set(GPIO_PinState s) { HAL_GPIO_WritePin(_port, _pin, s); };
};

