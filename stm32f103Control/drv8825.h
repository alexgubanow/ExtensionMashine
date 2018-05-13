#pragma once
#include "pin.h"
class drv8825
{
public:
	struct pinsStruct
	{
		/*m1 config pin*/
		pin M0;
		/*m2 config pin*/
		pin M1;
		/*m3 config pin*/
		pin M2;
		/*SLEEP pin, 0 - enable, 1 - disable*/
		pin SLEEP;
		/*RESET pin, 0 - enable, 1 - disable*/
		pin RST;
		/*ENABLE pin, 0 - enable, 1 - disable*/
		pin EN;
		/*FAULT pin, 0 - enable, 1 - disable*/
		pin FAULT;
		/*DIR pin, setting direction of moving*/
		pin DIR;
	} _pins;
	/*state indicate state of drv8825*/
	unsigned char state;
	enum pinState
	{
		Enable = GPIO_PIN_RESET,
		Disable = GPIO_PIN_SET
	};
	enum movDir
	{
		Direct = 0,
		Reverse = 1
	};
	enum microStepRes
	{
		Full_step = 0,
		Half_step = 1,
		step1_4 = 2,
		step1_8 = 3,
		step1_16 = 4,
		step1_32 = 5
	};
	/*mRes storing current Microstep Resolution*/
	microStepRes _mRes;
	drv8825() {};
	drv8825(pinsStruct pins, microStepRes mRes)
	{
		_pins = pins;
		DisableDrv();
		Sleep(Disable);
		Reset(Disable);
		setRes(mRes);
	};
	void setRes(microStepRes mRes);
	inline void EnableDrv() { _pins.EN.set((GPIO_PinState)0); }
	inline void DisableDrv() { _pins.EN.set((GPIO_PinState)1); }
	inline void Sleep(pinState newState) { _pins.SLEEP.set((GPIO_PinState)newState); }
	inline void Reset(pinState newState) { _pins.RST.set((GPIO_PinState)newState); }
	inline void Direction(movDir newState) { _pins.DIR.set((GPIO_PinState)newState); }
	inline pinState getState() { state = _pins.FAULT.get(); return (pinState)state; };
	~drv8825() {};
};
