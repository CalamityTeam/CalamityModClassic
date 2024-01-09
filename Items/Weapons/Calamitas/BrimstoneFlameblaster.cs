using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Items;

namespace CalamityModClassic1Point2.Items.Weapons.Calamitas
{
	public class BrimstoneFlameblaster : ModItem
	{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Brimstone Flameblaster");
		}

	    public override void SetDefaults()
	    {
			Item.damage = 64;
			Item.DamageType = DamageClass.Ranged;
			Item.width = 50;
			Item.height = 18;
			Item.useTime = 20;
			Item.useAnimation = 20;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.noMelee = true; //so the item's animation doesn't do damage
			Item.knockBack = 3.5f;
			Item.UseSound = SoundID.Item34;
			Item.value = 500000;
			Item.rare = ItemRarityID.LightPurple;
			Item.autoReuse = true;
			Item.shoot = Mod.Find<ModProjectile>("BrimstoneBallFriendly").Type; //idk why but all the guns in the vanilla source have this
			Item.shootSpeed = 8.5f;
			Item.useAmmo = 23;
		}
	}
}