#include <iostream>

class KeyEvent {
public:
    char key;
    KeyEvent(char k) : key(k) {}
};

class KeyFilter {
public:
    static bool filter(KeyEvent event) {
        // �����ŃC�x���g���t�B���^�����O
        return event.key == 'a'; // 'a'�L�[�̃C�x���g�̂ݒʉ�
    }
};

int main() {
    KeyEvent event('a');
    if (KeyFilter::filter(event)) {
        std::cout << "�L�['a'��������܂����B" << std::endl;
    }
    return 0;
}