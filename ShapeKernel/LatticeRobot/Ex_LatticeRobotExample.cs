//
// SPDX-License-Identifier: CC0-1.0
//
// This example code file is released to the public under Creative Commons CC0.
// See https://creativecommons.org/publicdomain/zero/1.0/legalcode
//
// To the extent possible under law, LEAP 71 has waived all copyright and
// related or neighboring rights to this PicoGK example code file.
//
// THE SOFTWARE IS PROVIDED “AS IS”, WITHOUT WARRANTY OF ANY KIND, EXPRESS
// OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
//

using PicoGK;
using System.Numerics;

namespace PicoGKExamples
{
    ///////////////////////////////////////////////////////////////////////////
    // Below is a static class that implements a single static function
    // that can be called from Library::Go()

    class LatticeRobotExample
    {
        public static void Task()
        {
            var unitCellName = "LatticeRobot-Diamond_TPMS";
            ImplicitUnitCell unitCell = new (Path.Combine(@"..\..\..\LatticeRobot_Library\", unitCellName), 2);

            var box = unitCell.Bounds;  // centered at origin
            box.vecMin.X *= 5;
            box.vecMax.X *= 5;

            unitCell.AdjustParameters = (ImplicitUnitCell unitCell, Vector3 p) => 
            {
                double diamondToGyroid = Math.Clamp((p.X - box.vecMin.X) / (box.vecMax.X - box.vecMin.X), 0, 1);
                unitCell.SetParameter("gyroid", diamondToGyroid);
            };

            try
            {
                Voxels voxL = new(unitCell, box);

                Library.oViewer().SetGroupMaterial(0, "3291a0", 0f, 1f);
                Library.oViewer().Add(voxL);
            }

            catch (Exception e)
            {
                Library.Log($"Failed to run example: \n{e.Message}"); ;
            }
        }
    }
}

