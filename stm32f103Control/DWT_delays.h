#pragma once
#include <stm32f1xx_hal.h>
#include <stm32_hal_legacy.h>

#define    DWT_CYCCNT    *(volatile unsigned long *)0xE0001004
#define    DWT_CONTROL   *(volatile unsigned long *)0xE0001000
#define    SCB_DEMCR     *(volatile unsigned long *)0xE000EDFC

extern uint32_t SystemCoreClock;

void DWT_Init(void);
static __inline unsigned long long delta(unsigned long long t0, unsigned long long t1);
void DWT_delay(unsigned long us);

