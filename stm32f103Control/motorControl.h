#pragma once
#include <stm32f1xx_hal.h>
#include <stm32_hal_legacy.h>
#include "main.h"
class motorControl
{
public:
	enum direction
	{
		forward = 0,
		backward = 1
	};
	unsigned long pos;
	unsigned long long stepsToReach;
	direction Dir;
	unsigned short speed;
	void Run();
	void Stop();
	void Release();
	void Update();
	motorControl();
	motorControl(int sp);
	~motorControl();
};