﻿using UnityEngine;
using NUnit.Framework;

namespace MVC {

    public class ViewTests {

        [Test]
        public void CanHideViews() {
            // Arrange.
            var canvasGo = new GameObject();

            var controllerGoOne = new GameObject();
            controllerGoOne.transform.parent = canvasGo.transform;
            controllerGoOne.AddComponent<TestControllerOne>();
            AddPeerViews(controllerGoOne.transform);

            var viewGo = new GameObject();
            var view = viewGo.AddComponent<TestView>();
            viewGo.transform.parent = controllerGoOne.transform;

            // Act.
            view.Hide();

            // Assert.
            Assert.IsFalse(viewGo.activeInHierarchy);

            GameObject.DestroyImmediate(canvasGo);

        }

        [Test]
        public void CanShowViews() {
            // Arrange.
            var canvasGo = new GameObject();

            var controllerGoOne = new GameObject();
            controllerGoOne.transform.parent = canvasGo.transform;
            controllerGoOne.AddComponent<TestControllerOne>();
            AddPeerViews(controllerGoOne.transform);

            var viewGo = new GameObject();
            var view = viewGo.AddComponent<TestView>();
            viewGo.transform.parent = controllerGoOne.transform;

            // Act.
            view.Render();

            // Assert.
            Assert.IsTrue(viewGo.activeInHierarchy);

            GameObject.DestroyImmediate(canvasGo);

        }

        [Test]
        public void CanRenderWithManyControllers() {
            //Arrange.
            var canvasGo = new GameObject();

            var controllerGoOne = new GameObject();
            controllerGoOne.transform.parent = canvasGo.transform;
            controllerGoOne.AddComponent<TestControllerOne>();
            AddPeerViews(controllerGoOne.transform);

            var controllerGoTwo = new GameObject();
            controllerGoTwo.transform.parent = canvasGo.transform;
            controllerGoOne.AddComponent<TestControllerOne>();
            AddPeerViews(controllerGoTwo.transform);

            var viewGo = new GameObject();
            var view = viewGo.AddComponent<TestView>();
            viewGo.transform.parent = controllerGoOne.transform;

            // Act.
            view.Render();

            Assert.IsTrue(viewGo.activeInHierarchy);
            
            GameObject.DestroyImmediate(canvasGo);
        }

        [Test]
        public void ExclusiveControllerHidesOthers() {

            // Arrange First Controller and view.
            var canvasGo = new GameObject();
            var mvc = canvasGo.AddComponent<MVCFramework>();

            var controllerGoOne = new GameObject();
            controllerGoOne.transform.parent = canvasGo.transform;
            var controllerOne = controllerGoOne.AddComponent<TestExclusiveControllerOne>();
            controllerOne.Init((x) => { }, new TestModel(null));
            AddPeerViews(controllerGoOne.transform);

            var viewGOOne = new GameObject();
            var viewOne = viewGOOne.AddComponent<TestExclusiveViewOne>();
            viewGOOne.transform.parent = controllerGoOne.transform;

            // Arrange Second Controller and view.
            var controllerGoTwo = new GameObject();
            controllerGoTwo.transform.parent = canvasGo.transform;
            var controllerTwo = controllerGoTwo.AddComponent<TestExclusiveControllerTwo>();
            controllerTwo.Init((x) => { }, new TestModel(null));
            AddPeerViews(controllerGoTwo.transform);
            
            var viewGOTwo = new GameObject();
            var viewTwo = viewGOOne.AddComponent<TestExclusiveViewTwo>();
            viewGOTwo.transform.parent = controllerGoOne.transform;

            mvc.Initialize();

            // Act.
            controllerOne.Action<TestExclusiveControllerTwo>(
                "FunctionOnControllerTwo", 
                new TestModel(null)
            );

            Assert.IsTrue(viewGOTwo.activeInHierarchy);
            Assert.IsTrue(!viewGOOne.activeInHierarchy);
            
            GameObject.DestroyImmediate(canvasGo);
        }

        private void AddPeerViews(Transform parent) {
            var childOneGo = new GameObject();
            childOneGo.transform.parent = parent;
            childOneGo.AddComponent<TestView>();
            var childTwoGo = new GameObject();
            childTwoGo.transform.parent = parent;
            childTwoGo.AddComponent<TestView>();
            var childThreeGo = new GameObject();
            childThreeGo.transform.parent = parent;
            childThreeGo.AddComponent<TestView>();
        }
    }
}