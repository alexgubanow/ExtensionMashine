#include "comPort.h"
#include "main.h"
#include "endStop.h"
#include "drv8825.h"
#include "hx711.h"

extern motorControl mC;
extern endStop endStop1;
extern endStop endStop2;
extern HX711  hx711sd;
extern drv8825 drv;

comPort::comPort()
{
}

comPort::~comPort()
{
}
void comPort::parseInStr()
{
	UserRxBufferFS[strcspn((char*)UserRxBufferFS, "\n")] = '\0';
	jsmn_init(&p);
	resultCode = jsmn_parse(&p, (char*)UserRxBufferFS, strlen((char*)UserRxBufferFS), tokens, sizeof(tokens) / sizeof(tokens[0]));
	if (resultCode > 0)
	{
		char Key[1024];
		char Val[1024];
		for (int i = 1; i < resultCode - 1; i+=2) // resultCode == 0 => whole json string
		{
			uint16_t lengthKey = tokens[i].end - tokens[i].start;
			uint16_t lengthVal= tokens[i + 1].end - tokens[i + 1].start;
			if (lengthKey < 1024)
			{
				memcpy(Key, &UserRxBufferFS[tokens[i].start], lengthKey);
				memcpy(Val, &UserRxBufferFS[tokens[i + 1].start], lengthVal);
				Key[lengthKey] = '\0';
				Val[lengthVal] = '\0';
				if (strcmp(Key, "Comm") == 0)
				{
					strcpy(Comm, Val);
				}
				else if (strcmp(Key, "speed") == 0)
				{
					char buf[128];
					strcpy(buf, Val);
					mC.speed = strtoul(buf, NULL, 0);
					mC.Update();
				}
				else if (strcmp(Key, "pos") == 0)
				{
					char buf[128];
					strcpy(buf, Val);
					mC.pos = strtoul(buf, NULL, 0);
					mC.Update();
				}
				else if (strcmp(Key, "dir") == 0)
				{
					char buf[128];
					strcpy(buf, Val);
					mC.Dir = (motorControl::direction)strtoul(buf, NULL, 0);
					mC.Update();
				}
			}
		}
	}
}

void comPort::sendAnswer()
{
	if (strcmp(Comm, "hx711?") == 0)
	{
		char normStr[10];
		hx711sd.currVal = hx711sd.get_units(1);
		snprintf(normStr, 10, "%d", hx711sd.currVal);
		sendStr(Comm, "val", normStr, Wait);
		HAL_Delay(100);
	}
	else if (strcmp(Comm, "STAT?") == 0)
	{
		sendStr(Comm, "status", "true", Wait);
	}
	else if (strcmp(Comm, "mRun?") == 0)
	{
		mC.Run();
		sendStr(Comm, "status", "true", Wait);
	}
	else if (strcmp(Comm, "mRelease?") == 0)
	{
		mC.Release();
		sendStr(Comm, "status", "true", Wait);
	}
	else if (strcmp(Comm, "mStop?") == 0)
	{
		mC.Stop();
		sendStr(Comm, "status", "true", Wait);
	}
	else if (strcmp(Comm, "endStops?") == 0)
	{
		int endStopsState = endStop2.get() * 2  + endStop1.get();
		char normStr[10];
		snprintf(normStr, 10, "%d", endStopsState);
		sendStr(Comm, "val", normStr, Wait);
		HAL_Delay(100);
	}
}

USBD_StatusTypeDef comPort::sendStr(const char * commTx, const char * key, char * str, waitForTXtype waitForTX)
{
	USBD_StatusTypeDef result = USBD_OK;
	snprintf((char *)UserTxBufferFS, 32768, "{\"Comm\":\"%s\",\"%s\":%s}", commTx, key, str);
	USBD_CDC_SetTxBuffer(&hUsbDeviceFS, UserTxBufferFS, strlen((char *)UserTxBufferFS));
	USBD_CDC_TransmitPacket(&hUsbDeviceFS);
	if (waitForTX)
	{
		waitUntilTXend(&hUsbDeviceFS);
	}
	return result;
}

void comPort::waitUntilTXend(USBD_HandleTypeDef *pdev)
{
	USBD_CDC_HandleTypeDef   *hcdc = (USBD_CDC_HandleTypeDef *)pdev->pClassData;
	//while (hcdc->TxState == 1){}
}