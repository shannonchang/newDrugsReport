﻿ select A.*,B.* from (select NOTICE_SNO, MEETING_TYPE, MEETING_TIME, SETUP_REASON,MEETING_RECORD,INSPECT_REPORT, IS_INVITE from TB_MEETING_INFO ) A,
            (select WRITY_COMPLET, NOTICE_SNO, IS_INSPECT, count(*) as count from TB_COUNSELING_RECORD
            where WRITY_COMPLET = 'Y' group by WRITY_COMPLET, NOTICE_SNO, IS_INSPECT )B
            WHERE A.NOTICE_SNO = B.NOTICE_SNO  ORDER BY B.NOTICE_SNO;