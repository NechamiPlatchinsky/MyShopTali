using Entities;
using Moq;
using System.Threading.Tasks;
using Xunit;
using Entities;  
using Repositories;
using Microsoft.EntityFrameworkCore;
using Moq.EntityFrameworkCore;

public class UserRepositoryTests
{
    [Fact]
    public async Task GetUser_ValidCredentials_ReturnUser()
    {

        var user = new User { Email = "cx@aaaaa", Password = "aaa@aaqaaq1222" };
        var mockContext = new Mock<_326774742WebApiContext>();
        var users = new List<User>() { user };
        mockContext.Setup(x => x.Users).ReturnsDbSet(users);
        var userRepository = new UserRepository(mockContext.Object);
        var result = await userRepository.GetUserToLogin(user.Email, user.Password);
        Assert.Equal(user, result);
    }
    //[Fact]
    //public async Task Get_UserExists_ReturnsUser()
    //{
    //    // Arrange
    //    var mockContext = new Mock<_326774742WebApiContext>(); // Mock �� ��������
    //    var userToReturn = new User { UserId = 1, FirstName = "John", LastName = "Doe", Email = "john.doe@example.com" };

    //    // ���� �� �-DbSet ����� �� ������ ����� ���-id ��� 1
    //    mockContext.Setup(m => m.Users.FindAsync(It.IsAny<int>())).ReturnsAsync(userToReturn);

    //    var Reposetory = new UserRepository(mockContext.Object);

    //    // Act
    //    var result = await Reposetory.CreateUser(1); // ������ �������� �� id 1

    //    // Assert
    //    Assert.NotNull(result);  // ����� ������� �� null
    //    Assert.Equal(1, result.Id); // ����� ��-id ��� 1
    //    Assert.Equal("John", result.FirstName); // ����� ���� ����� ��� "John"
    //    Assert.Equal("Doe", result.LastName); // ����� ���� ����� ��� "Doe"
    //    Assert.Equal("john.doe@example.com", result.Email); // ����� �����"� ����
    //}

    [Fact]
    public async Task Get_UserDoesNotExist_ReturnsNull()
    {
        // Arrange
        var mockContext = new Mock<_326774742WebApiContext>();

        // ���� �� �-DbSet ����� null ���� �� ���� ����� �� �-id ������
        mockContext.Setup(m => m.Users.FindAsync(It.IsAny<int>())).ReturnsAsync((User)null);

        var Reposetory = new UserRepository(mockContext.Object);

        // Act
        var result = await Reposetory.Get(999); // ������ �������� �� id ��� ����

        // Assert
        Assert.Null(result); // ����� ������� ��� null ����� �� id ��� ����
    }


    //[Fact]
    //public async Task Post_User_ReturnsUser()
    //{
    //    // Arrange
    //    var mockContext = new Mock<MyShop214935017Context>(); // Mock �� ��������
    //    var userToAdd = new User
    //    {
    //        Id = 1,
    //        FirstName = "John",
    //        LastName = "Doe",
    //        Email = "john.doe@example.com",
    //        Password = "securepassword"
    //    };

    //    // Setup �� AddAsync �� ������ �� ������ �����
    //    //mockContext.Setup(m => m.Users.AddAsync(It.IsAny<User>(), default)).ReturnsAsync(userToAdd);

    //    // Setup �� SaveChangesAsync �� ������ 1 (�����, �������� �����)
    //    mockContext.Setup(m => m.SaveChangesAsync(default)).ReturnsAsync(1);

    //    var Reposetory = new UserRepository(mockContext.Object);

    //    // Act
    //    var result = await Reposetory.Post(userToAdd); // ������ �������� �� ������ ����

    //    // Assert
    //    Assert.NotNull(result);  // ����� ������� �� null
    //    Assert.Equal(1, result.Id); // ����� ��-id ����
    //    Assert.Equal("John", result.FirstName); // ����� ���� ����� ����
    //    Assert.Equal("Doe", result.LastName); // ����� ���� ����� ����
    //    Assert.Equal("john.doe@example.com", result.Email); // ����� �����"� ����

    //    // Verify ���������� ������� ���
    //    mockContext.Verify(m => m.Users.AddAsync(It.IsAny<User>(), default), Times.Once); // ����� �-AddAsync ���� ��� ���
    //    mockContext.Verify(m => m.SaveChangesAsync(default), Times.Once); // ����� �-SaveChangesAsync ���� ��� ���
    //}

    [Fact]
    public async Task Post_FailedToAddUser_ThrowsException()
    {
        // Arrange
        var mockContext = new Mock<_326774742WebApiContext>();
        var userToAdd = new User
        {
            UserId = 1,
            FirstName = "John",
            LastName = "Doe",
            Email = "john.doe@example.com",
            Password = "securepassword"
        };

        // Setup �� AddAsync �� ������ Exception
        mockContext.Setup(m => m.Users.AddAsync(It.IsAny<User>(), default)).ThrowsAsync(new System.Exception("Failed to add user"));

        var Reposetory = new UserRepository(mockContext.Object);

        // Act & Assert
        await Assert.ThrowsAsync<System.Exception>(async () => await Reposetory.Post(userToAdd)); // ����� ������� �����
    }

    //[Fact]
    //public async Task Put_UserExists_UpdatesUser()
    //{
    //    // Arrange
    //    var mockContext = new Mock<MyShop214935017Context>(); // Mock �� ��������
    //    var userToUpdate = new User
    //    {
    //        Id = 1,
    //        FirstName = "John",
    //        LastName = "Doe",
    //        Email = "john.doe@example.com",
    //        Password = "securepassword"
    //    };

    //    var updatedUser = new User
    //    {
    //        Id = 1,
    //        FirstName = "Jane",
    //        LastName = "Doe",
    //        Email = "jane.doe@example.com",
    //        Password = "newpassword"
    //    };

    //    // Setup �� Update �� ���� �� �����, �� ����� �� ������ ����
    //    //mockContext.Setup(m => m.Users.Update(It.IsAny<User>())).Returns(userToUpdate);
    //    //var mockSet = new Mock<DbSet<User>>();
    //    //mockContext.Setup(m => m.Users.Update(It.IsAny<User>())).Returns(mockSet.Object);


    //    // Setup �� SaveChangesAsync �� ������ 1 (������ �����)
    //    mockContext.Setup(m => m.SaveChangesAsync(default)).ReturnsAsync(1);

    //    var Reposetory = new UserRepository(mockContext.Object);

    //    // Act
    //    var result = await Reposetory.Put(1, updatedUser); // ������ �������� �� id 1 ������ ������ ����

    //    // Assert
    //    Assert.NotNull(result); // ����� ������� �� null
    //    Assert.Equal(1, result.Id); // ����� ��-id ���� ��� ����
    //    Assert.Equal("Jane", result.FirstName); // ����� ���� ����� ������ �-"Jane"
    //    Assert.Equal("jane.doe@example.com", result.Email); // ����� �����"� ������
    //    mockContext.Verify(m => m.Users.Update(It.IsAny<User>()), Times.Once); // ����� �-Update ���� ��� ���
    //    mockContext.Verify(m => m.SaveChangesAsync(default), Times.Once); // ����� �-SaveChangesAsync ���� ��� ���
    //}

    [Fact]
    public async Task Put_UserDoesNotExist_ThrowsException()
    {
        // Arrange
        var mockContext = new Mock<_326774742WebApiContext>();
        var userToUpdate = new User
        {
            UserId = 1,
            FirstName = "John",
            LastName = "Doe",
            Email = "john.doe@example.com",
            Password = "securepassword"
        };

        // Setup �� Update �� ���� �� �����, �� ����� �� ������ ����
        mockContext.Setup(m => m.Users.Update(It.IsAny<User>()));

        // Setup �� SaveChangesAsync �� ������ Exception
        mockContext.Setup(m => m.SaveChangesAsync(default)).ThrowsAsync(new System.Exception("Failed to save changes"));

        var Reposetory = new UserRepository(mockContext.Object);

        // Act & Assert
        await Assert.ThrowsAsync<System.Exception>(async () => await Reposetory.UpDateUser(1, userToUpdate)); // ����� ������� �����
    }
    

}


