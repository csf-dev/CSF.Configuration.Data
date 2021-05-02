# Extension methods to get DB connections

The `System.Configuration.ConfigurationManager` API in both the .NET Framework FCL & [available via NuGet](https://www.nuget.org/packages/System.Configuration.ConfigurationManager) provides functionality to get database connections from registered provider factories and connection strings.

This very small package provides some extension methods in order to make consumption of this API slightly easier.

_It is expected that version 1.1.0 of this project will be its last. .NET is moving away from XML configuration files in favour of JSON. `ConfigurationManager` is a legacy API now and whilst it is still supported, it is not favoured for new development._

## Usage

To get a database connection factory (a `System.Func<System.Data.IDbConnection>`) from a configuration manager, use the following:

```csharp
var connectionFactory = ConfigurationManager
    .ConnectionStrings["myDb"].CreateConnectionFactory()
```

To just get a connection straight away, use the following:

```csharp
var connection = ConfigurationManager
    .ConnectionStrings["myDb"].CreateAndOpenConnection()
```

## Open source license
All source files within this project are released as open source software,
under the terms of [the MIT license].

[the MIT license]: http://opensource.org/licenses/MIT

This software is distributed in the hope that it will be useful, but please
remember that:

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.