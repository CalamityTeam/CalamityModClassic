using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Items;

namespace CalamityModClassic1Point2.Items {
public class BlossomPickaxe : ModItem
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Blossom Pickaxe");
	}
	
    public override void SetDefaults()
    {
        Item.damage = 70;
        Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
        Item.width = 56;
        Item.height = 60;
        Item.useTime = 1;
        Item.useAnimation = 15;
        Item.useTurn = true;
        Item.pick = 275;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.knockBack = 7.5f;
        Item.value = 287000;
        Item.UseSound = SoundID.Item1;
        Item.autoReuse = true;
        Item.tileBoost += 8;
    }
    
    public override void ModifyTooltips(List<TooltipLine> list)
    {
        foreach (TooltipLine line2 in list)
        {
            if (line2.Mod == "Terraria" && line2.Name == "ItemName")
            {
                line2.OverrideColor = new Color(0, 255, 200);
            }
        }
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(null, "UeliaceBar", 7);
        recipe.AddTile(TileID.LunarCraftingStation);
        recipe.Register();
    }

    public override void MeleeEffects(Player player, Rectangle hitbox)
    {
        if(Main.rand.NextBool(5))
        {
        	int dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, DustID.CursedTorch);
        }
    }
    
    public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
    {
		target.AddBuff(BuffID.OnFire, 200);
		target.AddBuff(BuffID.CursedInferno, 200);
	}
}}