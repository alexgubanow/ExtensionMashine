#pragma once
#include <stm32f1xx_hal.h>
#include <stm32_hal_legacy.h>
#include "main.h"
class motorControl
{
public:
	unsigned long pos;
	unsigned long long stepsToReach;
	unsigned char Dir;
	unsigned short speed;
	void Run();
	void Stop();
	void Release();
	void Update();
	motorControl();
	motorControl(int sp);
	~motorControl();
};