using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Items;

namespace CalamityModClassic1Point2.Items.Weapons
{
	public class BladecrestOathsword : ModItem
	{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Bladecrest Oathsword");
			//Tooltip.SetDefault("Sword of an ancient demon lord");
		}

		public override void SetDefaults()
		{
			Item.width = 58;
			Item.damage = 25;
			Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
			Item.useAnimation = 25;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.useTime = 25;
			Item.knockBack = 4f;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = false;
			Item.height = 58;
			Item.value = 100000;
			Item.rare = ItemRarityID.Orange;
			Item.shoot = Mod.Find<ModProjectile>("BloodScythe").Type;
			Item.shootSpeed = 6f;
		}
	
	    public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
	    {
			target.AddBuff(BuffID.OnFire, 200);
		}
	}
}
