using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Items.Armor;

namespace CalamityModClassic1Point2.Items.Armor {
[AutoloadEquip(EquipType.Head)]
public class VictideHelmet : ModItem
{
    public override void SetStaticDefaults()
    {
        //DisplayName.SetDefault("Victide Helmet");
        //Tooltip.SetDefault("5% increased summon damage");
    }

    public override void SetDefaults()
    {
        Item.width = 18;
        Item.height = 18;
        Item.value = 40000;
        Item.rare = ItemRarityID.Green;
        Item.defense = 1; //8
    }

    public override bool IsArmorSet(Item head, Item body, Item legs)
    {
        return body.type == Mod.Find<ModItem>("VictideBreastplate").Type && legs.type == Mod.Find<ModItem>("VictideLeggings").Type;
    }

    public override void UpdateArmorSet(Player player)
    {
        player.setBonus = "Increased life regen and summon damage while submerged in liquid\n" +
        	"Summons a sea urchin to protect you";
        CalamityPlayer modPlayer = player.GetModPlayer<CalamityPlayer>();
		modPlayer.urchin = true;
		if (player.whoAmI == Main.myPlayer)
		{
			if (player.FindBuffIndex(Mod.Find<ModBuff>("Urchin").Type) == -1)
			{
				player.AddBuff(Mod.Find<ModBuff>("Urchin").Type, 3600, true);
			}
			if (player.ownedProjectileCounts[Mod.Find<ModProjectile>("Urchin").Type] < 1)
			{
				Projectile.NewProjectile(player.GetSource_FromThis(), player.Center.X, player.Center.Y, 0f, -1f, Mod.Find<ModProjectile>("Urchin").Type, 0, 0f, Main.myPlayer, 0f, 0f);
			}
		}
        player.ignoreWater = true;
        if(player.wet == true || player.honeyWet == true || player.lavaWet == true)
    	{
        	player.GetDamage(DamageClass.Summon) += 0.15f;
        	player.lifeRegen += 3;
        }
    }
    
    public override void UpdateEquip(Player player)
    {
        player.GetDamage(DamageClass.Summon) += 0.05f;
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(null, "VictideBar", 3);
        recipe.AddTile(TileID.Anvils);
        recipe.Register();
    }
}}