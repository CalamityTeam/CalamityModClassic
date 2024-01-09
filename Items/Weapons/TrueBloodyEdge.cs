using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassic1Point2.Items.Weapons {
public class TrueBloodyEdge : ModItem
{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("True Bloody Edge");
			//Tooltip.SetDefault("Chance to heal the player on enemy hits\nFires a bloody blade");
		}

	public override void SetDefaults()
	{
		Item.width = 46;  //The width of the .png file in pixels divided by 2.
		Item.damage = 88;  //Keep this reasonable please.
		Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;  //Dictates whether this is a melee-class weapon.
		Item.useAnimation = 24;
		Item.useStyle = ItemUseStyleID.Swing;
		Item.useTime = 24;  //Ranges from 1 to 55.
		Item.knockBack = 6f;  //Ranges from 1 to 9.
		Item.UseSound = SoundID.Item1;
		Item.autoReuse = true;  //Dictates whether the weapon can be "auto-fired".
		Item.height = 54;  //The height of the .png file in pixels divided by 2.
		Item.value = 560000;  //Value is calculated in copper coins.
		Item.rare = ItemRarityID.Yellow;  //Ranges from 1 to 11.
		Item.shoot = Mod.Find<ModProjectile>("BloodyBlade").Type;
		Item.shootSpeed = 11f;
	}

	public override void AddRecipes()
	{
		Recipe recipe = CreateRecipe();
		recipe.AddIngredient(null, "BloodyEdge");
		recipe.AddIngredient(ItemID.BrokenHeroSword);
        recipe.AddTile(TileID.MythrilAnvil);	
        recipe.Register();
	}

    public override void MeleeEffects(Player player, Rectangle hitbox)
    {
        if (Main.rand.NextBool(5))
        {
        	int dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, DustID.Blood);
        }
    }
    
    public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
	{
    	if (target.type == NPCID.TargetDummy)
		{
			return;
		}
    	int healAmount = (Main.rand.Next(6) + 1);
    	if (Main.rand.NextBool(2))
    	{
    		player.statLife += healAmount;
    		player.HealEffect(healAmount);
    	}
	}
}}
