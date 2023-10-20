# 무정무계획: 성지키기 게임

무정무계획 팀에 의해 개발된 "성지키기"는 매 라운드마다 몰려오는 몬스터들로부터 성을 지키는 디펜스 게임입니다.

## 팀원

- 강성호: 플레이어 및 아이템 관련 시스템 구현
- 조범준: 씬 구현 ,UI, 사운드 및 이펙트 시스템 구현
- 김민태: 맵 구현, 몬스터 및 NPC 관련 시스템 구현

## 설명

"성지키기"는 팀 무정무계획이 유니티를 활용하여 개발한 게임으로, 플레이어는 몬스터들로부터 성을 지켜야 합니다. 각 팀원은 다음과 같은 역할을 담당했습니다:

- 강성호 플레이어와 아이템 관련 시스템을 구현하여 플레이어의 경험을 향상시켰습니다.
- 조범준은 씬, UI, 사운드 및 게임 이펙트 시스템 구현 및 디테일 관리를 하여 게임의 사용자 경험을 더욱 흥미롭게 만들었습니다.
- 김민태는 게임 맵, 몬스터, 및 NPC 시스템을 구현하여 게임 환경을 다채롭고 생동감 있게 만들었습니다.

"성지키기"는 플레이어에게 즐거운 게임 경험을 제공하며, 각 라운드가 더욱 도전적이고 흥미로워집니다. 팀 무정무계획은 게임 개발에 열정적으로 참여하여 이 프로젝트를 가능하게 했습니다.


# 기능 구현 설명

## 강성호


### 플레이어 

##### 스탯
- 스크립터블 오브젝트로 캐릭터의 베이스 스텟을 잡고 추가 스텟 구현시 플레이어 컨디션 스크립트에서 조정
##### 전투
```
if (TryGetComponent(out PlayerInput playerInput))
{
	InputAction action = playerInput.actions.FindAction("Move");
	action.performed += CallOnMove;
	...
}
```
- 인풋 시스템을 활용하여 플레이어의 이동, 시선처리, 공격 및 기타 상호작용 등을 조작
```
public void OnHit()
    {
	Ray ray = _cam.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        isHit = Physics.Raycast(ray, out RaycastHit hit, attackDistance);
        if (isHit)
        {
		if (hit.collider.TryGetComponent(out Monster monster))
            {
		if (monster != null)
                {
                    monster.TakeDamage(damage);
                    GameManager.Instance.garphicManager.MonsterHit(hit.point);
                }
            }
        }
    }
```
- 공격 시 화면 정중앙에서 레이를 쏴 맞은 상대의 태그 확인후 데미지 처리
- 공격 애니메이션 구현
### 아이템
##### 아이템 스탯
- 스크립터블 오브젝트로 아이템의 종류 및 기능을 구현


![아이템](https://github.com/tjdgh7419/Chapter3-3_B07_Project/assets/100994140/6ad02c8a-5406-4ea8-96b7-12e4bd1a8dc0)


##### 인벤토리
```
public void OnInventoryButton(InputAction.CallbackContext context)
public void AddItem(ItemData item)
{
	if (item.canStack)
	{
		ItemSlot slotInfo = GetItemStack(item);
		if (slotInfo != null)
		{				
			slotInfo.quantity++;
			UpdateUI();
			return;
		}
	}

	ItemSlot emptySlotInfo = GetEmptySlot();

	if (emptySlotInfo != null)
	{
		...
		UpdateUI();
		return;
	}
	return;
}
private ItemSlot GetItemStack(ItemData item)
...
```
- 인벤토리 스크립트에서 현재 플레이어가 가지고 있는 아이템데이터를 받아와 UI에 나타내며 각각의 아이템의 종류마다 사용 및 장착, 장착헤제 기능을 구현


![인벤토리2](https://github.com/tjdgh7419/Chapter3-3_B07_Project/assets/100994140/d44d2c64-5a70-4025-942c-ba857d013908)



##### 제작 
```
public bool MakableChk()
{		
	Inventory inventoryData = GameManager.Instance.inventory;
	bool chk = false;
	for (int i = 0; i < selectedCraftItem.item.resources.Length; i++)
	{
		chk = false;
		for (int j = 0; j < inventoryData.slots.Length; j++)
		{
			if (selectedCraftItem.item.resources[i] == inventoryData.slots[j].item)
			{
				if (selectedCraftItem.item.resourceCount[i] <= inventoryData.slots[j].quantity)
				{
					chk = true;
					break;					
				}
				else
				{
					return true;
				}
			}			
		}
	}
	if (!chk) return true;
	else return false;
}
```
- NPC와 상호 작용시 제작창을 띄우며 제작에 필요한 아이템은 인벤토리 내에 있는 아이템들을 사용하고 있으며 만약 아이템이 없거나 부족 할 시 제작에 불가능하게 구현

![캡처3](https://github.com/tjdgh7419/Chapter3-3_B07_Project/assets/100994140/93f68c34-33e3-439b-bb7e-8add9c346d05)


### 게임매니저 관리
게임내에 있는 모든 매니저 스크립트를 게임매니저에서 관리

## 조범준

### 씬

##### 시작 씬
- 게임 시작시 나타낼 시작 씬 구현
##### 로딩 씬
- 씬 전환시 중간에 로딩상태를 나타내기 위한 로딩씬 구현
### UI

##### UI매니저
```
    public bool IsOnUI;
    private void InitUIList()
    {
	...
        - 자신의 하단에 있는 UI오브젝트들을 저장
    }

    public T OpenUI<T>()
    {
	...
        - 저장한 오브젝트의 활성화를 위한 OpenUI 함수
    }

    public void MouseUnlock()
    {
        
    }
	...
```
- 마우스 활성 여부를 정할 함수 제작
- 비활성화 시 사용될 Close 함수 구현
- UI가 켜져 있을때를 확인하기 위한 IsOnUI 다른 많은 스크립트에서 불려서 사용됨
##### 플레이어 UI
```
    IEnumerator TakeDamage(string type = null)
    {
        yield return null;

        if (type == "MP")
        {
            while (true)
            {
    		...
            }
        }
        else
        {
            while (true)
            {
                ...
            }
        }
    }
...
```
- 코루틴을 활용하여 체력과 마나의 닳는 것을 보여주어 시각적인 재미를 부여함
- 현재 플레이어 HP를 받아와 즉각적인 반응 관찰 가능
- 피격시 나타나는 UI 구현


![제목 없음2](https://github.com/tjdgh7419/Chapter3-3_B07_Project/assets/100994140/894531ee-2709-446d-a2ee-80910b228116)



##### 환경설정 UI
```
    public void OnPreferencesSave()
    {
        UIManager.Instance.SetAudioSetting(MasterSlider.value, MusicSlider.value, EffactSlider.value);
        UIManager.Instance.SetGraphicSetting(EffactToggle.isOn, ShadowToggle.isOn);
    }

    public void CallAudioSetting()
    {
        float[] audios = UIManager.Instance.GetAudioSetting();
        MasterSlider.value = audios[0];
        MusicSlider.value = audios[1];
        EffactSlider.value = audios[2];
    }

    public void CallGraphicSetting()
    {
        bool[] graphic = UIManager.Instance.GetGraphicSetting();
        EffactToggle.isOn = graphic[0];
        ShadowToggle.isOn = graphic[1];
    }
...
```  
- 패널의 닫힘과 씬에 따른 행동을 취함
- 그래픽/ 오디오 버튼을 누르면 패널을 교체해주는 역할
- 게임 종료 버튼을 누르면 팝업창을 띄워서 종료 여부를 파악
- UIManager에 설정값을 저장하여 씬이 넘어가도 설정이 불려올수있도록 구현
- 게임 종료 버튼 구현

![캡처3](https://github.com/tjdgh7419/Chapter3-3_B07_Project/assets/100994140/9e14b778-2539-487a-bcaa-a8222c745419)


##### 퀘스트 UI
- NPC가 주는 퀘스트 및 현재 진행중인 퀘스트 목록 UI 구현
- 포기 버튼 클릭시 현재 진행중인 퀘스트 목록에서 퀘스트 삭제

![캡처](https://github.com/tjdgh7419/Chapter3-3_B07_Project/assets/100994140/9db38e56-39d2-4250-bce0-9b472533a411)  ![캡처2](https://github.com/tjdgh7419/Chapter3-3_B07_Project/assets/100994140/33bdac52-e5fb-4db7-8843-9e03bded09a5)




##### UI 팝업창
```
    public void SetAction(string _headingText, string _explanationText, Action onConfirm = null)
    {
        headingText.text = _headingText;
        explanationText.text = _explanationText;

        OnConfirm = onConfirm;
    }
...
  ```  
- UI내에서의 상호작용시 팝업창을 띄움
- SetAction 으로 텍스트들과 함수를 받아와서 확인버튼을 누르면 해당 함수가 실행되게 구현
#### 사운드
- 사운드 매니저를 구현하여 현재 게임 내에 있는 모든 사운드 관리
#### 이펙트

##### 이펙트 매니저
- 게임 내에 있는 이펙트 관리
- Start부분에서 이펙트 / 쉐도우 활성 여부에 따른 오브젝트 풀링 , 쉐도우 끄고 키기 진행
##### 전투 이펙트
- 공격시 검에서 나올 검기 파티클로 제작
- 마법 검 사용시 나타나는 파티클 제작
- 피격시 나타날 파티클 제작
- 동영상
### 기타 디테일 관리
- 시각적인 다채로움을 위해 로딩 씬과 시작씬에 맵 및 카메라 무빙 구현
- 플레이시의 재미를 느끼게 하기 위해 게임 맵 조정

## 김민태

### 게임 씬 구현
- 실제 플레이 될 게임 씬의 전반적인 느낌 및 배칭 시스템을 통한 드로우콜 줄이는 작업
### NPC
- 기본 NPC의 정보를 스크립티블 오브젝트에 저장하여 NPC 스크립트가 정보값을 받아와서 사용
- 플레이어가 근처에 올 시 상호 작용 준비를 통하여 플레이어의 상호작용 인풋액션에 반응할 수 있게 함
##### 퀘스트 NPC
- 플레이어와의 상호 작용시 현재 NPC가 가지고 있는 퀘스트 정보를 퀘스트패널에 띄움
##### 제작 NPC
- 플레이어와의 상호 작용시 제작창을 띄움
```
public void StartInteract()
    {
        if(npcSO.interact && npcAI == NPCAIState.Interact)
        {
            UIManager.Instance.MouseUnlock();
            if(window.TryGetComponent<QuestPanel>(out QuestPanel panel))
            {
                panel.quest = this.gameObject.GetComponent<Quest>();
            }
            window.SetActive(true);
            UIManager.Instance.IsOnUI = true;
            canTalk = false;
        }
    }
```
### 퀘스트
- 퀘스트 스크립트를 통하여 퀘스트의 정보를 저장하고 각각의 퀘스트NPC가 퀘스트를 저장함
##### 퀘스트 매니저
- 현재 플레이어가 진행중인 퀘스트를 딕셔너리를 통하여 관리 함
- 퀘스트 클리어시 플레이어에게 보상 제공

### 몬스터

##### 몬스터 AI
- 네비매시를 활용하여 현재 플레이어와의 거리에 따른 몬스터의 행동을 결정
```
 protected virtual void Update()
    {
        playerDistance = Vector3.Distance(this.gameObject.transform.position, player.transform.position);
        switch (aiState)
        {
            case MobAIState.Idle:
                if (playerDistance < detectDistance)
                {
                    aiState = MobAIState.Battle;
                    agent.isStopped = false;
                    animator.SetBool("run", true);
                }
                break;
            case MobAIState.Battle:
                AttackingUpdate();
                break;
            case MobAIState.Run:
                if (playerDistance < detectDistance)
                {
                    aiState = MobAIState.Battle;
                }
                if (Vector3.Distance(this.transform.position, agent.destination) <= 0.1f)
                {
                    agent.isStopped = true;
                    animator.SetBool("run", false);
                    aiState = MobAIState.Idle;
                }
                break;
        }
    }
```
##### 기본 웨이브 몬스터
- 각각의 라운드 마다 몬스터가 생성되며 기본적으로는 성을 부수기 위해 돌진하는것이 일반적
##### 네임드 몬스터
- 라운드 마다 생성되는 몬스터와 달리 필드에 존재하는 몬스터로 플레이어가 가까이 오거나 공격시 전투 시작
- 퀘스트와 연동되어 처치시 퀘스트가 있다면 퀘스트 클리어가 가능하도록 구현
##### 보스 몬스터
- 특정 라운드 마다 소환되며 강력한 공격과 체력을 가지고 있음

## 팀원들의 소감 및 한마디
- 강성호 :
- 조범준 :
- 김민태 : 일도 많고 탈도 많고 계획도 정말 없을줄은 몰랐지만 아무튼 완성해서 다행입니다 ㅎㅎ.
영상 링크 : [링크 텍스트](https://youtu.be/w1At6CJ7U2M)
# 감사합니다
