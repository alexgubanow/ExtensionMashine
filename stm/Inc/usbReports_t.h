#pragma once

#ifdef __cplusplus_cli
public enum class usbReports_t
{
#else
typedef enum usbReports_t
{
#endif // __cplusplus_cli
	getVer = 1,
	IOctl,
	TMCstatus,
	velocity,
	ReportMCUtemp,
	loadDefaults,
	saveToFLASH,
	DRVCTRLreport,
	CHOPCONFreport,
	SMARTENreport,
	SGCSCONFreport,
	DRVCONFreport,
	setHX711gain,
	getHX711val
}usbReports_t;