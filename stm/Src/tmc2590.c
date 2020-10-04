#include "tmc2590.h"
#include "spi.h"
#include "nvm.h"

unsigned int TMC2590readResponse;

void TMC2590_dispatcRXbuff(unsigned int val);

void TMC2590_WriteConfig()
{
	TMC2590_writeReg(tmc2590_CHOPCONF, Params.CHOPCONF_r.w);
	TMC2590_writeReg(tmc2590_SGCSCONF, Params.SGCSCONF_r.w);
	TMC2590_writeReg(tmc2590_DRVCONF, Params.DRVCONF_r.w);
	TMC2590_writeReg(tmc2590_DRVCTRL, Params.DRVCTRL_r.w);
	TMC2590_writeReg(tmc2590_SMARTEN, Params.SMARTEN_r.w);
}

void TMC2590_SPI_write(unsigned int val)
{
	unsigned int dataToSend = __RBIT(val & 0xFFFFF);
	LL_SPI_Enable(SPI1);
	while (!LL_SPI_IsActiveFlag_TXE(SPI1) && !LL_SPI_IsActiveFlag_RXNE(SPI1)) {}
	LL_GPIO_ResetOutputPin(SPI1_CSN_GPIO_Port, SPI1_CSN_Pin);
	LL_SPI_TransmitData16(SPI1, dataToSend >> 12);
	LL_SPI_TransmitData16(SPI1, dataToSend >> 22);
	unsigned int rx1 = LL_SPI_ReceiveData16(SPI1);
	unsigned int rx2 = LL_SPI_ReceiveData16(SPI1);
	while (LL_SPI_IsActiveFlag_BSY(SPI1)) {}
	LL_GPIO_SetOutputPin(SPI1_CSN_GPIO_Port, SPI1_CSN_Pin);
	LL_SPI_Disable(SPI1);
	TMC2590_dispatcRXbuff((__RBIT(rx1) >> 22) << 10 | __RBIT(rx2) >> 22);
}


void TMC2590_writeReg(tmc2590regs_enum addr, unsigned int val)
{
	TMC2590_SPI_write(addr << 17 | val);
}

void TMC2590_dispatcRXbuff(unsigned int val)
{
	TMC2590readResponse = val;
}

void setMotorVel(unsigned int vel)
{
	if (vel == 0)
	{
		LL_TIM_CC_DisableChannel(TIM14, 1);
	}
	else if (!startTrigger && !endTrigger)
	{
		unsigned int newVel = 65536 - vel;
		LL_TIM_SetAutoReload(TIM14, newVel);
		LL_TIM_OC_SetCompareCH1(TIM14, newVel / 2);
		if (!LL_TIM_IsEnabledCounter(TIM14))
		{
			LL_TIM_EnableCounter(TIM14);
		}
		if (!LL_TIM_CC_IsEnabledChannel(TIM14, 1))
		{
			LL_TIM_CC_EnableChannel(TIM14, 1);
		}
	}
}