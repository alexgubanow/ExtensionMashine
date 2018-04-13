#include "drv8825.h"

void drv8825::setRes(microStepRes mRes)
{
	_mRes = mRes;
	switch (mRes)
	{
	case drv8825::Full_step:
		_pins.M0.set(GPIO_PIN_RESET);
		_pins.M1.set(GPIO_PIN_RESET);
		_pins.M2.set(GPIO_PIN_RESET);
		break;
	case drv8825::Half_step:
		_pins.M0.set(GPIO_PIN_SET);
		_pins.M1.set(GPIO_PIN_RESET);
		_pins.M2.set(GPIO_PIN_RESET);
		break;
	case drv8825::step1_4:
		_pins.M0.set(GPIO_PIN_RESET);
		_pins.M1.set(GPIO_PIN_SET);
		_pins.M2.set(GPIO_PIN_RESET);
		break;
	case drv8825::step1_8:
		_pins.M0.set(GPIO_PIN_SET);
		_pins.M1.set(GPIO_PIN_SET);
		_pins.M2.set(GPIO_PIN_RESET);
		break;
	case drv8825::step1_16:
		_pins.M0.set(GPIO_PIN_RESET);
		_pins.M1.set(GPIO_PIN_RESET);
		_pins.M2.set(GPIO_PIN_SET);
		break;
	case drv8825::step1_32:
		_pins.M0.set(GPIO_PIN_SET);
		_pins.M1.set(GPIO_PIN_SET);
		_pins.M2.set(GPIO_PIN_SET);
		break;
	default:
		_pins.M0.set(GPIO_PIN_RESET);
		_pins.M1.set(GPIO_PIN_RESET);
		_pins.M2.set(GPIO_PIN_RESET);
		break;
	}
}
