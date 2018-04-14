#include "comPort.h"
#include "main.h"
#include "endStop.h"
#include "drv8825.h"
#include "hx711.h"

extern HX711 hx711sd;
extern drv8825 drv;
extern motorControl mC;

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
		char keyString[1024];
		char Prev_keyString[1024];
		for (int i = 1; i <= resultCode - 1; i++) // resultCode == 0 => whole json string
		{
			jsmntok_t key = tokens[i];
			uint16_t length = key.end - key.start;
			if (length < 1024)
			{
				memcpy(keyString, &UserRxBufferFS[key.start], length);
				keyString[length] = '\0';
				if (strcmp(Prev_keyString, "comm") == 0)
				{
					strcpy(commRx, keyString);
				}
				else if (strcmp(Prev_keyString, "speed") == 0)
				{
					char buf[128];
					strcpy(buf, keyString);
					mC.speed = strtoul(buf, NULL, 0);
				}
				else if (strcmp(Prev_keyString, "pos") == 0)
				{
					char buf[128];
					strcpy(buf, keyString);
					mC.pos = strtoul(buf, NULL, 0);
				}
				strcpy(Prev_keyString, keyString);
			}
		}
	}
}

void comPort::sendAnswer()
{
	if (strcmp(commRx, "hx711?") == 0)
	{
		int asfds = hx711sd.get_units(1);
		char normStr[10];
		snprintf(normStr, 10, "%d", asfds);
		sendStr(commRx, "hx711", normStr, Wait);
	}
	else if (strcmp(commRx, "STAT?") == 0)
	{
		sendStr(commRx, "status", "true", Wait);
	}
	else if (strcmp(commRx, "mRun?") == 0)
	{
		mC.Run();
		sendStr(commRx, "status", "true", Wait);
	}
	else if (strcmp(commRx, "mRelease?") == 0)
	{
		mC.Release();
		sendStr(commRx, "status", "true", Wait);
	}
	else if (strcmp(commRx, "mStop?") == 0)
	{
		mC.Stop();
		sendStr(commRx, "status", "true", Wait);
	}
}

USBD_StatusTypeDef comPort::sendStr(const char * commTx, const char * key, char * str, waitForTXtype waitForTX)
{
	USBD_StatusTypeDef result = USBD_OK;
	snprintf((char *)UserTxBufferFS, 32768, "{\"comm\":\"%s\",\"%s\":%s}", commTx, key, str);
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