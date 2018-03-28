#include "DWT_delays.h"


void DWT_Init(void)
{
	//разрешаем использовать счётчик
	SCB_DEMCR |= CoreDebug_DEMCR_TRCENA_Msk;
	//обнуляем значение счётного регистра
	DWT_CYCCNT = 0;
	//запускаем счётчик
	DWT_CONTROL |= DWT_CTRL_CYCCNTENA_Msk;
}
static __inline unsigned long long delta(unsigned long long t0, unsigned long long t1)
{
	return (t1 - t0);
}
void DWT_delay(unsigned long us)
{
	unsigned long long t0 = DWT->CYCCNT;
	unsigned long long us_count_tic = us * (SystemCoreClock / 1000000);
	while (delta(t0, DWT->CYCCNT) < us_count_tic);
}

