#include "pin.h"

pin::pin(GPIO_TypeDef * port, unsigned short pin)
{
	if (port != 0 && pin != 0)
	{
		_port = port;
		_pin = pin;
		state = HAL_GPIO_ReadPin(_port, _pin);
	}
}