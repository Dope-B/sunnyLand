# SunnyLand

#### 사용기술: Unity(2020.1.6f1 버전업)
#### 제작기간: 2018.08.05~2018.11.13
<p>
<img width="30%" src="https://user-images.githubusercontent.com/33209821/229788373-7e86ff8f-048d-4351-b336-cdfff861c627.png"/>
<img width="30%" src="https://user-images.githubusercontent.com/33209821/229788378-813c022c-6d5f-4caf-89c4-b36a76683f67.png"/>
<img width="30%" src="https://user-images.githubusercontent.com/33209821/229788383-62544005-7be0-4d6d-8860-eaa1caba7cf5.png"/>
<br/>
<img width="30%" src="https://user-images.githubusercontent.com/33209821/229788388-8c3a812a-2a38-47ed-9649-557c155efd58.png"/>
<img width="30%" src="https://user-images.githubusercontent.com/33209821/229788360-62efeaa9-eb1e-4198-8765-cad321a96d02.png"/>
<img width="30%" src="https://user-images.githubusercontent.com/33209821/229788370-f4ea8edf-aa96-4de3-9f7b-0acb75ed794a.png"/>
</p>

#### 테스트 영상
[![Video Label](http://img.youtube.com/vi/zAMmNaqJxCA/0.jpg)](https://www.youtube.com/watch?v=zAMmNaqJxCA&ab_channel=Dope_B)
<br/>

#### 설명
- 게임매니저 객체는 싱글톤패턴 사용
- 카메라는 Lerp함수를 사용하여 캐릭터를 추적하지만 clamp함수로 특정 위치에서만 움직인다.
- 맵은 타일맵 브러쉬로 만
- 아이템 습득 또는 피격 시 플로팅 텍스트가 출력되는데 일정 시간이 지나면 destroy되도록 함

```C#
destroyTime -= Time.unscaledDeltaTime;
if (destroyTime <= 0)
{
   Destroy(this.gameObject);
   break;
}

```
- 플레이어의 컬라이더는 피격용, 공격용(발에 위치), 사다리용(캐릭터 가운데 위치)으로 이루어져 있다.
- 사다리를 타고있을 시 사다리 끝에 도달했다면 자동으로 점프가 된다.

```C#
if (player.GSanimator.GetBool("isClimbing"))
{
     setClimbingFalse();// 상태 변경
     player.GSrigid.velocity = Vector3.zero;// 속도 초기화
     player.GSrigid.AddForce(new Vector2(0,1.5f), ForceMode2D.Impulse);// 강제프점프
}

```
- 대화기능 및 대화 중 분기 선택 기능 구현(기본 대화기능 -> dialogueManager, 분기 선택기능-choiceManager)
- 대화 시 한글자씩 출력되지만 대화 키를 누르면 내용이 한꺼번에 출력됨
- 실질적인 대화 스크립트는 NPC_dialogue를 통해 이루어진다.
```C#
public class Dialogue {// 대화 구성 요소

    public string[] sentences;// 대화 내용
    public Sprite[] portraits;// 대화 시 이미지
    public Sprite[] dialogueBoxs; // 대화 박스 
}

public class choice {// 분기 대화 구성 요소
    public string question;// 분기 질문
    public string[] answers;// 대답
}
```

- 크랭크를 이용해서 떠있는 땅을 움직이게 할 수 있다.

```C#
public floating[] floatings;// 크랭크는 떠있는 땅을 배열로 저장함
```

- 움직이는 땅은 움직이는 시간, 방향, 속도 등 을 설정 할 수 있다.
- 맵에 있는 아이템들은 애니메이터를 통해 위아래로 움직인다.
- 자주 쓰일 함수들은 DataBase 스크립트에 넣어놨다.(플로팅 텍스트, 아이템 사용 함수)
- 아이템 획득 시 getItem함수를 통해 플로팅 텍스트를 띄운다. 그리고 아이템 리스트에 중복된 아이템이 없을 시 새로 추가하고 중복된 아이템이 있을 시 카운트를 더한다.(inventory.cs)
- 아이템 구성요소는 다음과 같다.

```C#
    public int itemID;
    public string itemName;
    public string itemDescription;
    public int itemCount;
    public Sprite itemIcon;
    public ItemType itemType;//타입
    public item_use effect;// 사용효과
    public enum ItemType
    {
        Equip,Use,Quest
    }

```
- 인벤토리는 I키를 눌러 활성화 한다. 
- 인벤토리는 3개의 탭으로 이루어져 있고 키보드 조작으로 설정을 변경 할 수 있다.
- 각 선택된 아이템이나 탭은 SelectedItem()과 SelectedTab()으로 투명도를 조절하여 포커스됨을 보여준다.
- 선택된 아이템을 사용한다면 useRequest 객체를 통해 선택창이 출력되고 종류에 따라 다른 코드를 실행한다.

```C#
IEnumerator use(string up, string down)// 약식으로 표현
    {
        go_select.SetActive(true);// 선택창 활성화
        go_select.transform.position = slots[selectedItem].transform.position;// 선택창 위치정조정
        useRequest.show(up, down);// 선택지 출력 및 결과값 저장
        yield return new WaitUntil(() => !useRequest.activated);
        if (useRequest.getResult())
        {
                for (int i = 0; i < inventoryItemList.Count; i++)
                {
                    // 선택된 아이템 종류에 따라 실행
            }
        }
        go_select.SetActive(false);// 선택 완료 후 비활성
    }
```
- 장비창의 작동방식은 아이템 창의 방식과 유사하다.
- 맵 이동은 페이드인,아웃으로 이루어진다.
- 플레이어 hp가 0 이하로 내려간다면 특정 건물에서 다시 리스폰된다.
- 리스폰 건물에서 엔터키를 누르면 맵 이동이 가능하다.

#### 피드백
- 엔진은 처음 사용해서 쓸데없는 함수와 스크립트가 많음
- 에셋 파일 정리 필요
- 게임 내 오브젝트 정리 필
- 해상도 고려가 안 되어있음
- 사다리 이동 시 수직동기화 처리가 안 되어있음
- 타일맵 사이에 선이 보여서 텍스쳐를 입혀봤지만 완전히 없어지지 않음
- 경사도가 있는 땅 위를 지나갈 시 물리처리가 어색함
- 점프 시 떠있는 땅과 충돌 무시 기능 필요
- 독수리와 식물 AI 개선이 필요
- 상호작용 키 인식 개선 필요
- 백그라운드 스크롤링 기능 추가 필요
