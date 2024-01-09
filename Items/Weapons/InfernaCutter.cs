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
	public class InfernaCutter : ModItem
	{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Inferna Cutter");
		}

	    public override void SetDefaults()
	    {
	        Item.damage = 85;
	        Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
	        Item.width = 60;
	        Item.height = 46;
	        Item.useTime = 16;
	        Item.useAnimation = 16;
	        Item.useTurn = true;
	        Item.axe = 27;
	        Item.useStyle = ItemUseStyleID.Swing;
	        Item.knockBack = 7.75f;
	        Item.value = 500000;
	        Item.rare = ItemRarityID.LightPurple;
	        Item.UseSound = SoundID.Item1;
	        Item.autoReuse = true;
	    }
	
	    public override void AddRecipes()
	    {
	        Recipe recipe = CreateRecipe();
	        recipe.AddIngredient(null, "PurityAxe");
	        recipe.AddIngredient(ItemID.SoulofFright, 8);
	        recipe.AddIngredient(null, "EssenceofChaos", 3);
	        recipe.AddTile(TileID.MythrilAnvil);
	        recipe.Register();
	    }
	    
	    public override void MeleeEffects(Player player, Rectangle hitbox)
	    {
	        if (Main.rand.NextBool(4))
	        {
	        	int dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, DustID.Torch);
	        }
	    }
	    
	    public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
		{
	    	target.AddBuff(BuffID.OnFire, 300);
	    	if(Main.rand.NextBool(2))
	    	{
	    		target.AddBuff(Mod.Find<ModBuff>("BrimstoneFlames").Type, 300);
	    	}
		}
	}
}