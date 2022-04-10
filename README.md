# Mini Adbrix
코딩 테스트 제출용 사용자 이벤트 수집 솔루션입니다.
총 5개의 프로젝트로 이루어져 있으며 일부 프로젝트는 `AWS`를 사용합니다.

## Using AWS
- EC2( Collect.Api )
- EC2( Search.Api )
- RDS( MySQL )
- SQS
- Lambda( CollectWorker )

## Project Description
### Collect.Api
사용자 이벤트를 수신하고 `AWS SQS`로 전달하는 `ASP.Net Core` 프로젝트입니다.
EC2 인스턴스에 배포되며 사용자 이벤트 수신 API를 제공합니다.

### CollectWorker
`AWS SQS`는 큐에 수집되는 메시지를 `AWS Lambda`로 트리거합니다.
CollectWorker는 `AWS Lambda`에 배포되는 람다 함수 프로젝트이며 전달받은 사용자 이벤트를 `AWS RDS`에 저장합니다.

### Search.Api
사용자 이벤트 조회 API를 제공하는 `ASP.Net Core` 프로젝트로 EC2 인스턴스에 배포됩니다.

### Programmers
임의의 사용자 이벤트를 발생시키는 콘솔 프로그램입니다.
프로그래머스 사용자를 예시로 제작했습니다.

#### 동작
- 사용자 로그인
- 문제 접속
- 답안 제출( 랜덤 성공 / 실패 )
    - 성공: 랜덤 다른 문제 접속 / 종료
    - 실패: 알고리즘 강의 시청
        - '답안 제출'로 돌아가서 반복

#### 사용
서버 주소와 동작 봇 개수를 실행 인자로 전달하거나, 실행 후 수동으로 입력합니다.
```
Programmers.exe http://server/ 5

or

Programmers.exe
Api base uri: http://server/
Bot count: 5
```

## API
#### api/collect
사용자 이벤트 데이터를 전송하는 API입니다.
```json
{
    "event_id" : "EVENT_ID",
    "user_id" : "USER_ID",
    "event" : "EVENT_TYPE",
    "parameters" : {
        // JSON 형식의 이벤트 정보
    }
}

=>

{
    "is_success": true
}
```

#### api/search
사용자 이벤트 목록을 최근순으로 조회하는 API입니다.
```json
{
    "user_id": "USER_ID"
}

=>

{
    "is_success": true,
    "result": [
        {
            "event_id" : "EVENT_ID",
            "event" : "EVENT_TYPE",
            "parameters" : {
                // JSON 형식의 이벤트 정보
            },
            "event_datetime": "EVENT_DATE"
        },
        {
            "event_id" : "EVENT_ID",
            "event" : "EVENT_TYPE",
            "parameters" :
            {
                // JSON 형식의 이벤트 정보
            },
            "event_datetime": "EVENT_DATE"
        }
    ]
}
```