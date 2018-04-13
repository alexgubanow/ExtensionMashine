#include "JSONpacker.h"

void stringfyCharArray(unsigned char * arr, size_t arrSize, char * str, unsigned char incStep)
{
	str[0] = '\0';
	sprintf(str, "[");
	for (int i = 0; i < arrSize; i += incStep)
	{
		sprintf(str + strlen(str), "%u,", arr[i]);
	}
	str[strlen(str) - 1] = ']';
}

void stringfyShortArray(unsigned short * arr, size_t arrSize, char * str, unsigned char incStep)
{
	str[0] = '\0';
	sprintf(str, "[");
	for (int i = 0; i < arrSize; i += incStep)
	{
		sprintf(str + strlen(str), "%u,", arr[i]);
	}
	str[strlen(str) - 1] = ']';
}
