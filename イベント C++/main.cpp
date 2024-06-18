#include <iostream>

class KeyEvent {
public:
    char key;
    KeyEvent(char k) : key(k) {}
};

class KeyFilter {
public:
    static bool filter(KeyEvent event) {
        // ここでイベントをフィルタリング
        return event.key == 'a'; // 'a'キーのイベントのみ通過
    }
};

int main() {
    KeyEvent event('a');
    if (KeyFilter::filter(event)) {
        std::cout << "キー'a'が押されました。" << std::endl;
    }
    return 0;
}