# Virtual Reality Basics and Practice Term Project

> **2023년 1학기 가상현실 기초 텀 프로젝트**
> Unity와 Google Cardboard XR을 활용하여 제작한 **무한 진행형 VR 러너 게임**입니다.

플레이어는 VR 헤드셋의 움직임을 이용해 캐릭터를 좌우로 조작하면서 자동으로 전진합니다.
진행 중 등장하는 차량을 피하고 다양한 아이템을 활용하여 최대한 오래 생존하는 것을 목표로 합니다.

---

## 프로젝트 개요

본 프로젝트는 가상현실 환경에서의 **시선 기반 인터랙션**, **HMD 움직임을 이용한 조작**, **VR UI**, **게임 오브젝트의 동적 생성 및 관리**를 직접 구현해 보기 위해 제작되었습니다.

일반적인 키보드·게임패드 중심 조작 대신 VR 환경에 적합하도록 카메라/HMD의 회전값을 플레이어 이동에 활용했으며, 메뉴 선택 또한 화면 중앙의 gaze cursor를 이용할 수 있도록 구성했습니다.

게임이 시작되면 플레이어는 지속적으로 앞으로 이동하며 시간이 지날수록 속도가 증가합니다. 도로에는 차량과 아이템이 동적으로 생성되고, 맵 역시 플레이 진행에 맞춰 계속해서 교체됩니다.

---

## 주요 기능

### VR 기반 플레이어 조작

* 플레이어 자동 전진
* 카메라/HMD 회전값을 이용한 좌우 이동
* 플레이 영역을 벗어나지 않도록 좌우 이동 범위 제한
* 시간이 지남에 따라 이동 속도 증가
* 최대 이동 속도 제한

게임 시작 시 이동 속도는 `4.0`이며, 게임 진행 중 일정 시간마다 속도가 증가하여 최대 `10.0`까지 상승합니다.

---

### Gaze 기반 UI

화면 중앙에서 Raycast를 사용하여 사용자가 바라보고 있는 UI를 판별합니다.

지원하는 인터랙션:

* 게임 시작 버튼
* BGM 선택
* Gaze Gauge를 이용한 선택
* 클릭을 이용한 즉시 선택

버튼을 일정 시간 바라보거나 입력을 수행하면 해당 기능이 실행됩니다.

---

### 차량 장애물 시스템

도로 위에 차량이 지속적으로 생성되어 플레이어의 진행을 방해합니다.

* 여러 차량 Prefab 중 하나를 랜덤 선택
* 여러 Spawn Point 중 하나를 랜덤 선택
* 약 `1.2초` 간격으로 차량 생성
* 차량의 지속적인 직선 이동
* 일정 거리를 벗어난 차량 자동 제거
* 플레이어와 충돌 시 Game Over

사용이 끝난 차량을 제거하여 계속 진행되는 게임에서 불필요한 GameObject가 누적되는 것을 방지합니다.

---

### 동적 맵 생성

플레이어가 앞으로 이동함에 따라 새로운 맵이 계속 생성됩니다.

* 5종류의 Map Prefab 사용
* 다음 맵을 랜덤으로 선택
* 일정 거리마다 새로운 맵 생성
* 플레이어 뒤쪽의 오래된 맵 자동 제거
* 동시에 유지되는 맵 수 제한
* 통과한 맵 수를 게임 기록으로 표시

이를 통해 제한된 수의 Map Prefab만으로 계속 진행할 수 있는 형태의 스테이지를 구성했습니다.

---

### 아이템 시스템

게임 중 총 4종류의 아이템이 등장합니다.

| 아이템               | 기능                   |
| ----------------- | -------------------- |
| Jump Item         | 플레이어에게 위쪽 힘을 적용하여 점프 |
| Shield Item       | 차량과의 충돌을 한 번 방어      |
| Attack Item       | 시선 방향의 차량을 공격하여 제거   |
| Deceleration Item | 플레이어의 이동 속도를 감소      |

아이템은 맵에 따라 랜덤하게 생성되며 획득 시 Particle Effect를 재생한 뒤 제거됩니다.

---

### HUD

플레이 중 현재 게임 상태를 실시간으로 확인할 수 있습니다.

표시 정보:

* 플레이 시간
* 지나간 맵/마을 수
* 현재 이동 속도

---

### Game Over

Shield가 없는 상태에서 차량과 충돌하면 게임이 종료됩니다.

게임 종료 화면을 표시한 뒤 일정 시간이 지나면 메인 Scene을 다시 로드하여 게임을 재시작합니다.

---

## 기술 스택

| 구분                    | 사용 기술                             |
| --------------------- | --------------------------------- |
| Game Engine           | Unity 2021.3.17f1                 |
| Language              | C#                                |
| VR/XR                 | Google Cardboard XR Plugin        |
| Additional XR Package | Oculus XR Plugin                  |
| UI                    | Unity UI, TextMeshPro             |
| Platform              | Android                           |
| Interaction           | HMD/Camera Rotation, Gaze Raycast |
| Physics               | Unity Rigidbody / Collider        |

---

## 프로젝트 구조

```text
Virtual_Reality_Basics_And_Practice_Term_Project/
├── Assets/
│   ├── AssetStore/
│   ├── Plugins/
│   ├── Prefabs/
│   │   ├── Cars/
│   │   ├── Items/
│   │   ├── Maps/
│   │   ├── Particles/
│   │   └── ...
│   ├── Resources/
│   ├── Samples/
│   ├── Scenes/
│   │   └── SampleScene.unity
│   └── Script/
│       ├── CarMoving.cs
│       ├── CarSpawn.cs
│       ├── ItemCtrl.cs
│       ├── ItemSpawn.cs
│       ├── MapManager.cs
│       └── PlayerCtrl.cs
├── Packages/
└── ProjectSettings/
```

---

## 주요 스크립트

### `PlayerCtrl.cs`

플레이어와 관련된 핵심 게임 로직을 담당합니다.

* VR 기반 좌우 이동
* 자동 전진
* 시간에 따른 가속
* Gaze Cursor와 UI Raycast
* 게임 시작
* BGM 선택
* 아이템 사용
* 차량 충돌 처리
* HUD 업데이트
* Game Over 및 재시작

### `MapManager.cs`

플레이 진행에 따라 맵을 동적으로 관리합니다.

* 랜덤 Map Prefab 생성
* 플레이어 위치 기반 다음 맵 생성
* 지나간 Map 제거
* 현재 Map 개수 관리

### `CarSpawn.cs`

차량 장애물을 생성합니다.

* Spawn Point 랜덤 선택
* 차량 Prefab 랜덤 선택
* 일정 시간마다 차량 생성

### `CarMoving.cs`

생성된 차량의 이동과 제거를 관리합니다.

### `ItemSpawn.cs`

맵에 아이템을 확률적으로 배치합니다.

### `ItemCtrl.cs`

아이템의 회전 애니메이션과 플레이어 획득 처리를 담당합니다.

---

## 실행 방법

### 1. Repository Clone

```bash
git clone https://github.com/Lithium07z/Virtual_Reality_Basics_And_Practice_Term_Project.git
```

### 2. Unity에서 프로젝트 열기

권장 Unity 버전:

```text
Unity 2021.3.17f1
```

Unity Hub에서 저장소 폴더를 프로젝트로 추가한 뒤 실행합니다.

### 3. Scene 열기

```text
Assets/Scenes/SampleScene.unity
```

### 4. Android 환경 준비

Google Cardboard 기반 VR 실행을 위해 Unity Hub에서 Android Build Support가 필요합니다.

필요에 따라 다음 모듈을 함께 설치합니다.

```text
Android Build Support
Android SDK & NDK Tools
OpenJDK
```

### 5. Build & Run

Android 기기를 연결한 뒤 Unity의 Build Settings에서 Android를 선택하고 Build & Run을 수행합니다.

---

## 조작

| 동작             | 조작               |
| -------------- | ---------------- |
| 좌우 이동          | HMD/카메라 회전       |
| UI 선택          | Gaze 유지 또는 Click |
| Jump Item 사용   | 입력               |
| Attack Item 사용 | 차량을 바라본 상태에서 입력  |
| Shield         | 획득 후 자동 적용       |
| Deceleration   | 획득 후 자동 적용       |

---

## 프로젝트 발표 자료

2023년 1학기 최종 텀 프로젝트 발표 자료는 아래 링크에서 확인할 수 있습니다.

[최종 텀 프로젝트 결과 발표 PPT](https://www.canva.com/design/DAFjjdR_uuw/Uq_qUS_RDAlRgJxt7NcMGw/view?utm_content=DAFjjdR_uuw&utm_campaign=designshare&utm_medium=link&utm_source=editor)

---

## 참고

이 저장소는 대학 수업의 가상현실 텀 프로젝트 결과물을 보관하기 위해 공개되었습니다.

프로젝트에는 Unity Asset Store 및 외부 패키지에서 가져온 리소스가 포함되어 있을 수 있으므로, 각 외부 에셋과 패키지의 저작권 및 라이선스는 원 저작자의 정책을 따릅니다.
