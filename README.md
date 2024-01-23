# Sparta_UnityBasic
스파르타 유니티 입문 개인과제

## 프로젝트 설명
Zep을 모방한 스파르타 타운 만들기

### 기능 설명

#### StartScene

<details>
<summary>캐릭터 이름 설정</summary>
<div markdown="1">

- NameChange UI Prefab 을 통해 변경
    
  ![NamePrefab](https://github.com/JY-LemongO/Sparta_UnityBasic/assets/122505119/556c5319-4845-42d0-8419-96523e817754)
  - Prefab 사용 이유 : 첫 캐릭터 생성하는 StartScene, 인 게임 영역 MainScene 둘 다 사용하는데 기능의 차이가 전혀 없기 때문에 Prefab화 하였음.
  - Prefab 구성
    - 이름을 입력하는 InputField
    - 확인 버튼 Button
    - 올바르지 않은 입력 알림 Text
    - 위 모든것을 제어하는 NameUI Script
  - NameUI Script
    - TMP_InputField(입력 및 확인), Button(확인), TextMeshProUGUI(알림 텍스트) [SerializeField] 로 선언 및 인스펙터에서 할당
   
  ![NameUIInspector](https://github.com/JY-LemongO/Sparta_UnityBasic/assets/122505119/58f12d5f-6a06-4d9e-8194-ff1003ce3350)

<pre><code>[SerializeField] TMP_InputField _nameInput;
[SerializeField] Button _applyButton;
[SerializeField] TextMeshProUGUI _noticeText;

private void Start()
{
    _nameInput.onEndEdit.AddListener(OnInputField);
    _applyButton.onClick.AddListener(OnApplyBtn);
}
</code></pre>

  - InputField 와 Button 의 이벤트를 Start 함수에서 등록하게 하였음. (인스펙터 드래그 드롭 방식보다 눈에 잘 보여서 이렇게 작업.)
  - 올바른 입력 시
    - (싱글톤)GameManager의 전역 string 변수 PlayerName에 _nameInput.text 를 할당.
    - MainScene 으로 씬 전환, Player의 Setup함수 호출로 string 변수 _name에 PlayerName에 할당.

  ![112](https://github.com/JY-LemongO/Sparta_UnityBasic/assets/122505119/f7c84250-c23e-4a31-a742-2002de024ae6)
  위 그림과 같이 진행
  
  - [현재 리포지 NameUI.cs 링크](https://github.com/JY-LemongO/Sparta_UnityBasic/blob/main/Assets/Scripts/Contents/UI/NameUI.cs)

  *정확한 과정은 다음과 같다.*
  
  *1. InputField의 입력값 GameManager 전역변수 PlayerName에 할당*
  
  *2. 할당과 동시에 MainScene으로 이동*
  
  *3. GameManager에 static Player가 존재. 해당 Player를 Get 할 때 Setup 이 최초 1회 실행된다.*
  
  *4. MainScene에선 MainCamera가 Update로 GameManager.Player를 지속 Get(Cam Follow)*
  
  *5. Player.Setup 에서 string _name 에 할당 및 Player 하위의 TMP _nameText.text 에 할당하여 이름 출력*
  

</div>
</details>


<details>
<summary>캐릭터 (애니메이션) 설정</summary>
<div markdown="2">

- ManualSelector UI Prefab 을 통해 변경
  
  ![CharacterChanger](https://github.com/JY-LemongO/Sparta_UnityBasic/assets/122505119/67395e1c-e6b5-4f21-a5b8-30af323d28a9)
  - Prefab 구성
    - 각 캐릭터를 선택할 수 있는 EventTrigger    
    - ManualSelect Script
  - ManualSelect Script
    - EventTrigger의 이벤트로 할당할 함수 OnManualChangeCharacter(int index)
<pre><code>public void OnManualChangeCharacter(int index)
{
    if (SceneManager.GetActiveScene().name == "StartScene")
        GameManager.Instance.SetAnimator(index);
    else
    {            
        UIManager.IsChangerOpen = false;
        GameManager.Player.ChangeState(PlayerState.Idle);
        GameManager.Player.ChangeAnimator(index);
    }            
    
    gameObject.SetActive(false);
}
</code></pre>

 - StartScene 에선 Player가 가지고있는 Animator 배열의 인덱스 정보만 할당
   - 이름변경과 마찬가지로 MainScene 전환 시 Player.Setup으로 Animator 변경
 - MainScene 에선 직접적인 Player의 Animator 변경 호출

</div>
</details>


#### MainScene

<details>
<summary>캐릭터 이동</summary>
<div markdown="3">

- Player , InputManager Script 이용

  ![Player](https://github.com/JY-LemongO/Sparta_UnityBasic/assets/122505119/45a0764b-19a5-483f-a8d8-d631747438ca)
- Player.PlayerMove() 에서 전반적인 움직임 로직 담당
- Setup 시 InputManager의 _onMovePlayer event에 구독
- InputManager 에서 FixedUpdate로 _onMovePlayer 호출
- [현재 리포지 InputManager.cs 링크](https://github.com/JY-LemongO/Sparta_UnityBasic/blob/main/Assets/Scripts/Contents/InputManager.cs)
- [현재 리포지 Player.cs 링크](https://github.com/JY-LemongO/Sparta_UnityBasic/blob/main/Assets/Scripts/Contents/Player.cs)

</div>
</details>


<details>
<summary>방 만들기</summary>
<div markdown="4">

- TilePalette 활용하여 방 만듦

  ![tile](https://github.com/JY-LemongO/Sparta_UnityBasic/assets/122505119/126f5ba8-bf14-4008-9884-371bcdef4cfb)
- Collision, Props, TileMap 세가지 그리드로 구분.
  - Collision : 충돌구역
  - Props : 책상 등 잡동사니
  - TileMap : 바닥 및 벽

  ![map](https://github.com/JY-LemongO/Sparta_UnityBasic/assets/122505119/aec20286-1da2-4f26-a446-bbf3e2f055c6)

</div>
</details>


<details>
<summary>카메라 따라가기</summary>
<div markdown="5">

- CameraController Script를 MainCamera에 부착시켜 따라갈 수 있도록 하였음
<pre><code>private Transform _playerTransform;
[SerializeField] Vector2 _borderLeftDown;
[SerializeField] Vector2 _borderRightUp;
[SerializeField] float _cameraSpeed;
private void Start()
{
    _playerTransform = GameManager.Player.transform;
}

private void FixedUpdate()
{        
    transform.position = Vector3.Lerp(transform.position, _playerTransform.position + Vector3.forward * -10, Time.deltaTime * _cameraSpeed);

    float limitX = Mathf.Clamp(transform.position.x, _borderLeftDown.x, _borderRightUp.x);
    float limitY = Mathf.Clamp(transform.position.y, _borderLeftDown.y, _borderRightUp.y);

    transform.position = new Vector3(limitX, limitY, -10);
}
</code></pre>

- Player의 Trasnform을 전역변수르 두고 Start 에서 GameManager 의 Player.Transform을 _playerTransform에 할당
  - 매번 GameManager 에서 호출하는 것 보단 전역으로 두는것이 좋다고 생각. 가독성에도 좋아보였음
- Vector3.Lerp 를 사용하여 플레이어 위치를 부드럽게 따라가도록 하였음
- _borderLeftDown, _borderRightUp Vector2 변수로 플레이어가 지정된 위치 밖일 때 카메라는 따라가지 못하도록 함

![gif용](https://github.com/JY-LemongO/Sparta_UnityBasic/assets/122505119/2052bb1c-4493-43d7-9e77-e9aa3742c97a)
![gif용 (1)](https://github.com/JY-LemongO/Sparta_UnityBasic/assets/122505119/105a481d-3742-46f6-9267-9ec0c81e32e2)

</div>
</details>


## ReadMe 내용은 필수요구사항만 담았습니다.
