# my-idle-game
2D 방치형 게임(모바일) | Unity 2022.3.62f2

# 개요
적을 처치하여 아이템을 얻고 강화하여 더 높은 단계를 노려보자!

패배헤도 지원금을 통해 더 강해지고 다시 싸운다!

## 주요 기능

### 로그인(파이어 베이스)
<img width="1076" height="438" alt="1" src="https://github.com/user-attachments/assets/41e93115-a42c-4403-a91c-66c453393f3e" />

![login](https://github.com/user-attachments/assets/8a895183-5518-46b7-b4f1-7be7440b7c00)

게스트 로그인

![guest](https://github.com/user-attachments/assets/cd41c094-66f8-463b-a362-2d6a37845c45)


이메일 로그인(실패)

![email_fail](https://github.com/user-attachments/assets/8eba0d71-9bef-45e5-9a9f-a28359d08c12)
![email_fail2](https://github.com/user-attachments/assets/aa26aed4-20e3-44ee-b0a7-c6bcafef2229)

이메일 로그인(성공)

![email_success](https://github.com/user-attachments/assets/ecaf7d94-84e6-405c-b212-76bae592a6c2)

### 게임 스타트!
![loading](https://github.com/user-attachments/assets/16be3bf7-c3c2-4497-8cd5-a3c36892284c)

업데이트로 이미지 변경

![change](https://github.com/user-attachments/assets/780866e2-d1b2-47ea-a7e6-fd6ea86b5990)

### 인게임 

적 처치시 아이템 드랍

![item](https://github.com/user-attachments/assets/aa46b91f-b5f4-4823-bc8a-522e4a996c3c)

적 처치시 100라운드 마다 추가 아이템 드랍

![2](https://github.com/user-attachments/assets/6ba714ff-e15a-4eca-bb18-40e730c178ba)

아이템 장착

![equip](https://github.com/user-attachments/assets/911725dc-5ef2-4c65-9e90-cb06bac83e21)

아이템 강화

![enhanced](https://github.com/user-attachments/assets/b2e6cdf3-80d0-4b59-aa45-e14b3016f149)

강화 실패 및 돈 부족시 경고!

![failed](https://github.com/user-attachments/assets/30b3b901-5ce5-4fec-ab97-cc838574ddef)


패배시 적 체력 회복 및 지원금(+ G)

![die](https://github.com/user-attachments/assets/a8887c9e-2db1-417e-a806-db15772bc13e)

위쪽의 장비창을 눌러 현재 스테이터스 확인

![1](https://github.com/user-attachments/assets/087fe4f4-c9fa-4744-b5df-97a618ac6803)


## 커밋 규칙
[Docs] 문서 및 파일 작업

[Chore] 코드 수정없이 작업

[Refactor] 코드 최적화

[Feat] 코드 수정 및 추가로 인한 기능 추가

[Fix] 버그 수정

## 코드 컨벤션
|접근자|변수|예시|bool 값|
|------|-----------|----------|----------|
|public| 파스칼 (첫문자 대문자) | public int MaxHp | public bool IsCount |
|private| _카멜 ( _ + 첫문자 소문자 이후 대문자) | private int _currentHp | private bool isCount |

* 클래스/구조체/메서드/enum 변수 => 파스칼 (ex. GameManager, private void GameOver(), public void LevelUp(), ItemType.Helmet )

* 지역 변수 => 카멜 (ex. int number )

