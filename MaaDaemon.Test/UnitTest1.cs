namespace MaaDaemon.Test {
    [TestClass]
    public class UnitTest1 {
        [TestMethod]
        public void TestMethod1() {
            var checker = new MaaChecker();
            var processname = checker.GetProcessFileName("notepad");
            Assert.AreEqual("C:\\Program Files\\WindowsApps\\Microsoft.WindowsNotepad_11.2312.18.0_x64__8wekyb3d8bbwe\\Notepad\\Notepad.exe", processname);
            Console.WriteLine(processname);
        }
    }
}