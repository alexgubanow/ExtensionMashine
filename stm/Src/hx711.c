#include "hx711.h"
#include "main.h"

unsigned char hx711gain = 1;
unsigned char isReadyHX711val = 0;
int hx711value = 0;

void readHX711()
{
	hx711value = 0;
	for (unsigned char i = 0; i < 24; i++)
	{
		LL_GPIO_SetOutputPin(HX711_CLK_GPIO_Port, HX711_CLK_Pin);
		hx711value = hx711value << 1;
		if (LL_GPIO_ReadInputPort(HX711_DOUT_GPIO_Port) & HX711_DOUT_Pin)
		{
			hx711value++;
		}
		LL_GPIO_ResetOutputPin(HX711_CLK_GPIO_Port, HX711_CLK_Pin);
	}
	for (unsigned char i = 0; i < hx711gain; i++)
	{
		LL_GPIO_SetOutputPin(HX711_CLK_GPIO_Port, HX711_CLK_Pin);
		LL_GPIO_ResetOutputPin(HX711_CLK_GPIO_Port, HX711_CLK_Pin);
	}
	hx711value = hx711value ^ 0x800000;
	isReadyHX711val = 1;
}
