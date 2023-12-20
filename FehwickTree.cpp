#include <iostream>
#include <vector>

class Fehwick_tree {
public:
    Fehwick_tree(int n) {
        this->n = n;
        tree.assign(n, std::vector<std::vector<int64_t>>(n, std::vector<int64_t>(n, 0)));
    }
    int n;
    std::vector<std::vector<std::vector<int64_t> > > tree;

    int64_t sum(int x, int y, int z) {
        int result = 0;
        for (int i = x; i >= 0; i = (i & (i + 1)) - 1) {
            for (int j = y; j >= 0; j = (j & (j + 1)) - 1) {
                for (int k = z; k >= 0; k = (k & (k + 1)) - 1) {
                    result += tree[i][j][k];
                }
            }
        }
        return result;
    }

    void modify(int x, int y, int z, int64_t delta) {
        for (int i = x; i < n; i = i | (i + 1)) {
            for (int j = y; j < n; j = j | (j + 1)) {
                for (int k = z; k < n; k = k | (k + 1)) {
                    tree[i][j][k] += delta;
                }
            }
        }
    }
};

int main()
{
    int n;
    std::cin >> n;
    Fehwick_tree fenwick_tree = Fehwick_tree(n);
    std::vector<int64_t> results(0);
    bool flag = true;
    while (flag) {
        int sign, x, y, z, d, x1, y1, z1, x2, y2, z2;
        std::cin >> sign;
        if (sign == 1) {
            std::cin >> x >> y >> z >> d;
            fenwick_tree.modify(x, y, z, d);
        }
        else if (sign == 2) {
            std::cin >> x1 >> y1 >> z1 >> x2 >> y2 >> z2;
            x1--; y1--; z1--;
            int64_t result = fenwick_tree.sum(x2, y2, z2) - fenwick_tree.sum(x1, y1, z1) -
                fenwick_tree.sum(x1, y2, z2) - fenwick_tree.sum(x2, y1, z2) - fenwick_tree.sum(x2, y2, z1) +
                fenwick_tree.sum(x2, y1, z1) + fenwick_tree.sum(x1, y2, z1) + fenwick_tree.sum(x1, y1, z2);
            results.push_back(result);
        }
        else {
            flag = false;
        }
    }

    for (int i = 0; i < results.size(); ++i) {
        std::cout << results[i] << "\n";
    }

    return 0;
}
