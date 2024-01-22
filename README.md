# Sparta_UnityBasic
스파르타 유니티 입문 개인과제

## 프로젝트 설명
Zep을 모방한 스파르타 타운 만들기

### 기능 설명

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

  *정확한 과정은 다음과 같다.*
  
  *1. InputField의 입력값 GameManager 전역변수 PlayerName에 할당*
  
  *2. 할당과 동시에 MainScene으로 이동*
  
  *3. GameManager에 static Player가 존재. 해당 Player를 Get 할 때 Setup 이 최초 1회 실행된다.*
  
  *4. MainScene에선 MainCamera가 Update로 GameManager.Player를 지속 Get(Cam Follow)*
  
  *5. Player.Setup 에서 string _name 에 할당 및 Player 하위의 TMP _nameText.text 에 할당하여 이름 출력*
  

</div>
</details>
