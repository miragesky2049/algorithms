class Solution(object):
    def towSum(self, nums, target):
        arr = []
        if len(nums) < 2:
            return arr
        for i in range(len(nums)):
            sub = target-nums[i]
            for j in range(i+1, len(nums), 1):
                if sub == nums[j]:
                    arr.append(i)
                    arr.append(j)
                    return arr
        return arr

st = Solution()
ret = st.towSum([1, 6, 2, 9, 4, 7, 5, 8], 6)
print ret
