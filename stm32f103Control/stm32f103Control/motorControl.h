#pragma once
#include <stm32f1xx_hal.h>
#include <stm32_hal_legacy.h>
class motorControl
{
public:
	unsigned long pos;
	unsigned long long stepsToReach;
	unsigned char Dir;

	motorControl();
	~motorControl();
};


//motorControl mC;