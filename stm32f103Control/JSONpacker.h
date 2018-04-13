#pragma once
#include <stm32f1xx_hal.h>
#include <stm32_hal_legacy.h>
#include <string.h>

void stringfyCharArray(unsigned char * arr, size_t arrSize, char * str, unsigned char incStep);
void stringfyShortArray(unsigned short * arr, size_t arrSize, char * str, unsigned char incStep);